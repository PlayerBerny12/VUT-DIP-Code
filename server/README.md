Build detection method
```
clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-ShallowCNN_lfcc_I -t dfdf-detection-audio-1
docker image tag dfdf-detection-audio-1 localhost:5000/dfdf-detection-audio-1
docker push localhost:5000/dfdf-detection-audio-1

clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-ShallowCNN_lfcc_O -t dfdf-detection-audio-2
docker image tag dfdf-detection-audio-2 localhost:5000/dfdf-detection-audio-2
docker push localhost:5000/dfdf-detection-audio-2

clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-ShallowCNN_mfcc_I -t dfdf-detection-audio-3
docker image tag dfdf-detection-audio-3 localhost:5000/dfdf-detection-audio-3
docker push localhost:5000/dfdf-detection-audio-3

clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-ShallowCNN_mfcc_O -t dfdf-detection-audio-4
docker image tag dfdf-detection-audio-4 localhost:5000/dfdf-detection-audio-4
docker push localhost:5000/dfdf-detection-audio-4

clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-TSSD_wave_I -t dfdf-detection-audio-5
docker image tag dfdf-detection-audio-5 localhost:5000/dfdf-detection-audio-5
docker push localhost:5000/dfdf-detection-audio-5

clear
cd C:\VUT-DIP\AudioDeepFakeDetection\
docker build . -f .\dockerfiles\Dockerfile-TSSD_wave_O -t dfdf-detection-audio-6
docker image tag dfdf-detection-audio-6 localhost:5000/dfdf-detection-audio-6
docker push localhost:5000/dfdf-detection-audio-6
```

Build API
```
cd C:\VUT-DIP\VUT-DIP-Code\server\api
docker build . -f .\DeepfakeDetectionFramework\Dockerfile -t dfdf-api
docker image tag dfdf-api localhost:5000/dfdf-api
docker push localhost:5000/dfdf-api
```

Build controller
```
cd C:\VUT-DIP\VUT-DIP-Code\server\processing_units
docker build . -t dfdf-controller
docker image tag dfdf-controller localhost:5000/dfdf-controller
docker push localhost:5000/dfdf-controller
```

Install RabbitMQ cluster operator
```
kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"
```

Create cluster
```
kubectl apply -f .
``` 

Container logs and exec
``` 
kubectl logs --follow <pod-name> --container dfdf-detection-audio-1
kubectl exec -it <pod-name> --container dfdf-detection-audio-1 -- /bin/bash
``` 