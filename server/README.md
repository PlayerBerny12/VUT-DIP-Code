Build API
```
docker build . -f .\DeepfakeDetectionFramework\Dockerfile -t dfdf-api
docker image tag dfdf-api localhost:5000/dfdf-api
docker push localhost:5000/dfdf-api
```

Build Controller
```
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