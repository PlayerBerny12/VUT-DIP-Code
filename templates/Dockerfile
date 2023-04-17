FROM python:alpine

WORKDIR /app
COPY entrypoint.py requirements.txt ./
RUN python3 -m pip install -r requirements.txt

CMD [ "uvicorn", "entrypoint:app", "--reload", "--port", "50501" ]