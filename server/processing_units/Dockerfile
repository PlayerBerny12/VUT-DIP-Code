FROM python:alpine

WORKDIR /app
COPY controller.py requirements.txt ./
RUN python3 -m pip install -r requirements.txt

CMD [ "python3", "-u", "controller.py" ]