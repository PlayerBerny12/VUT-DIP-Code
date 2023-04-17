import aiohttp
import asyncio
import os
import pika

PROCESSING_UNITS_URLS = [f"http://localhost:{port}/detect" for port in os.environ["ProcessingUnitsPorts"].split(":")]

async def http_get_async(session, url):
    async with session.get(url) as resp:
        json = await resp.json()
        return json

async def call_all_processing_units(urls):
    async with aiohttp.ClientSession() as session:
        tasks = []
        for url in urls:
            tasks.append(asyncio.ensure_future(http_get_async(session, url)))

        responses = await asyncio.gather(*tasks)
    return responses

def on_message_received(channel, method, properties, body):
    print(f"Received body: {body}")
    
    data = asyncio.run(call_all_processing_units(PROCESSING_UNITS_URLS))
    print(data)    

    channel.basic_ack(delivery_tag=method.delivery_tag)    

if __name__ == "__main__":
    connection_parameters = pika.ConnectionParameters(os.environ["RabbitMQ"])
    connection = pika.BlockingConnection(connection_parameters)
    queue = os.environ["RabbitMQQueue"]    

    channel = connection.channel()
    channel.queue_declare(queue=queue)
    channel.basic_qos(prefetch_count=1)
    channel.basic_consume(queue=queue, on_message_callback=on_message_received)

    print(f"Start consuming: {queue}")
    print(PROCESSING_UNITS_URLS)
    channel.start_consuming()