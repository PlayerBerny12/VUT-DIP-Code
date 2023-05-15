"""
DFDF (DeepfakeDetectionFramework)
Author: Jan Bernard (xberna18@stud.fit.vutbr.cz)
"""

import requests

from time import sleep

DFDF_ENDPOINT = "http://20.31.219.16"


def send_request(filename, file_path, file_extension):    
    if file_extension == ".mp4":
        file_type = "video/mp4"
    elif file_extension == ".wav":
        file_type = "audio/wav"

    file = {
        "file": (filename, open(file_path, "rb"), file_type),
    }
    
    response = requests.post(f"{DFDF_ENDPOINT}/api/detect/file", files=file)
    return int(response.text)


def get_responses(request_ID):
    while True:
        response = requests.get(
            f"{DFDF_ENDPOINT}/api/request/results", {"requestID": request_ID})

        if response.status_code == 200:
            break

        sleep(0.25)

    return response.json()


def map_responses(responses):
    stringBuilder = f"{responses['value']}"

    for response in responses["responses"]:
        stringBuilder += f";{response['value']}"

    return stringBuilder
