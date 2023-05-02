import requests

from time import sleep

DFDF_ENDPOINT = "http://localhost"

def send_request(filename, file_path):
    headers = {
        "Content-Type": "multipart/form-data"
    }
    file = {
        "file": (filename, open(file_path, "rb")),
    }
    
    response = requests.post(f"{DFDF_ENDPOINT}/api/detect/file", headers=headers, files=file)
    return int(response.text)

def get_responses(request_ID):
    while True:
        response = requests.get(f"{DFDF_ENDPOINT}/api/request/results", { "requestID": request_ID})

        if response.status_code == 200:
            break
        
        sleep(0.25)
    
    return response.json()

def map_responses(responses):
    stringBuilder = f"{responses.value}"

    for response in responses.responses:
        stringBuilder += f";{response}"

    return stringBuilder
    
