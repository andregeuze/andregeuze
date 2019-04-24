# Kubernetes training
Often called k8s, pronounced 'kates'.

## Day 1
We start with minikube helloworld: https://kubernetes.io/docs/tutorials/hello-minikube/
Launch the Terminal and play around with it.

Cheatsheet with all commands is located at:
https://kubernetes.io/docs/reference/kubectl/cheatsheet

Start an nginx container with:
`kubectl run mywebapp --image=nginx --restart=Never`

Generate a yaml file from a running pod with:
`kubectl get pod mywebapp -o yaml --export > mywebapp.yaml`

Generate a yaml file with:
`kubectl create deployment myfirstdeployment --dry-run -o yaml --image=busybox > myfirstdeployment.yaml`

Run the deployment with:
`kubectl apply -f .\myfirstdeployment.yaml`

Create a namespace with:
`kubectl create namespace *name*`

