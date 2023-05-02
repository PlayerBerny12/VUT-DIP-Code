from typing import Dict, Any

import aiohttp
import asyncio
import json
import os
import pika

PROCESSING_UNITS_URLS = [f"http://localhost:{port}/detect" for port in os.environ["ProcessingUnitsPorts"].split(":")]

async def http_get_async(session: Any, url: str, params: Dict[str, str]):
    async with session.get(url, params=params) as resp:
        if resp.status == 200:
            json = await resp.json()
            return json
        else :
            return None

async def call_all_processing_units(urls: str, params: Dict[str, str]):
    timeout = aiohttp.ClientTimeout(total=600)
    
    async with aiohttp.ClientSession(timeout=timeout) as session:
        tasks = []
        for url in urls:
            tasks.append(asyncio.ensure_future(http_get_async(session, url, params)))

        responses = await asyncio.gather(*tasks)
        responses = [i for i in responses if i is not None]
    return responses

def create_channel(queue_name: str):
    connection_parameters = pika.ConnectionParameters(os.environ["RabbitMQ"])
    connection = pika.BlockingConnection(connection_parameters)

    channel = connection.channel()
    channel.queue_declare(queue=queue_name)   

    return channel

def on_message_received(channel: Any, method: Any, _, body: bytes):
    params = body.decode("utf8").replace("\'", "\"")    
    params = json.loads(params)
    params_lowecase = {key.lower():value for key, value in params.items()}
    print(f"Received body: {params_lowecase}")    

    responses = asyncio.run(call_all_processing_units(PROCESSING_UNITS_URLS, params_lowecase))
    print(f"Received responses: {responses}")

    queue_output = os.environ["RabbitMQOutputQueue"]    
    channel_output = create_channel(queue_output)
    channel_output.basic_publish("", queue_output, json.dumps(responses))
    
    channel.basic_ack(delivery_tag=method.delivery_tag)    

def main():
    queue = os.environ["RabbitMQQueue"]
    channel = create_channel(queue)
            
    channel.basic_qos(prefetch_count=1)
    channel.basic_consume(queue=queue, on_message_callback=on_message_received)

    print(f"Start consuming: {queue}")
    channel.start_consuming()

if __name__ == "__main__":
    main()