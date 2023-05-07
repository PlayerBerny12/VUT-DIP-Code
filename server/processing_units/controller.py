from typing import Dict, Any

import aiohttp
import asyncio
import functools
import json
import os
import pika
import threading

PROCESSING_UNITS_URLS = [f"http://localhost:{port}/detect" for port in os.environ["ProcessingUnitsPorts"].split(":")]
CONNECTION_PARAMETERS = pika.ConnectionParameters(os.environ["RabbitMQ"])
CONNECTION = pika.BlockingConnection(CONNECTION_PARAMETERS)

async def http_get_async(session, url, params):
    try:
        async with session.get(url, params=params) as resp:
            if resp.status == 200:
                json = await resp.json()
                return json
            else :
                return None
    except:
        return None

async def call_all_processing_units(urls, params):
    timeout = aiohttp.ClientTimeout(total=900)
    
    async with aiohttp.ClientSession(timeout=timeout) as session:
        tasks = []
        for url in urls:
            tasks.append(asyncio.ensure_future(http_get_async(session, url, params)))

        responses = await asyncio.gather(*tasks)
        responses = [i for i in responses if i is not None]
    return responses

def create_channel(connection: Any, queue_name: str):
    channel = connection.channel()
    channel.queue_declare(queue=queue_name)   

    return channel

def fisnish_processing(connection, channel, delivery_tag, id, responses):
    queue_output = os.environ["RabbitMQOutputQueue"]    

    channel_output = create_channel(connection, queue_output)
    channel_output.basic_publish("", queue_output, json.dumps({"RequestID": id, "Responses": responses})) 
    channel_output.close()
    
    if channel.is_open:        
        channel.basic_ack(delivery_tag=delivery_tag)

def request_processing(connection, channel, delivery_tag, body):
    params = body.decode("utf8").replace("\'", "\"")
    params = json.loads(params)
    params_lowecase = {key.lower():value for key, value in params.items()}
    print(f"Received body: {params_lowecase}")    

    responses = asyncio.run(call_all_processing_units(PROCESSING_UNITS_URLS, params_lowecase))
    print(f"Received responses: {responses}")
    
    finish_cb = functools.partial(fisnish_processing, connection, channel, delivery_tag, params["ID"], responses)
    connection.add_callback_threadsafe(finish_cb)
    
def on_message_received(channel, method, properties, body):    
    t = threading.Thread(target=request_processing, args=(CONNECTION, channel, method.delivery_tag, body))
    t.start()    

def main():
    queue = os.environ["RabbitMQQueue"]
    channel = create_channel(CONNECTION, queue)
            
    channel.basic_qos(prefetch_count=1)
    channel.basic_consume(queue=queue, on_message_callback=on_message_received)

    print(f"Start consuming: {queue}")
    channel.start_consuming()

if __name__ == "__main__":
    main()