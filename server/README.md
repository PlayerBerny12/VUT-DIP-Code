Build API
```
docker build . -f .\DeepfakeDetectionFramework\Dockerfile -t dfdf
docker image tag dfdf localhost:5000/deepfakedetectionframeworkapi
docker push localhost:5000/deepfakedetectionframeworkapi
```

Install RabbitMQ cluster operator
```
kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"
```

Create cluster
```
kubectl apply -f .
```