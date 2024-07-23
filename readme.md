# Install microk8s

# Enable add-ons
- cert-manager
- dashboard
- dns
- ha-cluster
- ingress
- metallb
  - `microk8s enable metallb:192.168.1.200-192.168.1.230`



# Install services using Helm

- nfs subdir
- postgresql
- grafana
- influx
- rabbitmq
- grocy (plus ingress)
