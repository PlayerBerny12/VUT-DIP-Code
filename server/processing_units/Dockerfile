# DFDF (DeepfakeDetectionFramework)
# Author: Jan Bernard (xberna18@stud.fit.vutbr.cz)

FROM python:alpine

WORKDIR /app
COPY controller.py requirements.txt ./
RUN python3 -m pip install -r requirements.txt --no-cache-dir  

CMD [ "python3", "-u", "controller.py" ]