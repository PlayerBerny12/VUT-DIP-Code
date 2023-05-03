#!/bin/sh

# Delete DFDF
kubectl delete -R -f .

# Delete RabbitMQ cluster operator
kubectl delete -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"

# Delete Prometheus operator
kubectl delete -f "https://raw.githubusercontent.com/prometheus-operator/prometheus-operator/master/bundle.yaml"
