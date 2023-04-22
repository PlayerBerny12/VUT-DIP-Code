FROM python:alpine

WORKDIR /app
COPY entrypoint.py requirements.txt ./
RUN python3 -m pip install -r requirements.txt --no-cache-dir  

CMD [ "uvicorn", "entrypoint:app", "--host", "0.0.0.0", "--port", "50500", "--reload"]