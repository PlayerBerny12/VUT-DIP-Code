#!/bin/sh

# DFDF (DeepfakeDetectionFramework)
# Author: Jan Bernard (xberna18@stud.fit.vutbr.cz)

# Deploy RabbitMQ cluster operator
kubectl apply -f "https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml"

# Deploy Prometheus operator
kubectl create -f "https://raw.githubusercontent.com/prometheus-operator/prometheus-operator/master/bundle.yaml"

# Depoly DFDF rest
kubectl apply -R -f .
