---
layout: post
title: Changing IP Addresses of Nodes in a Proxmox Cluster
lead: How to safely change IP addresses of your Nodes in a running Proxmox cluster without breaking it.
author: André Geuze
categories: homeautomation
tags: proxmox cluster nodes ip corosync networking automation
---

## Introduction

Changing the IP addresses of a Proxmox cluster can seem daunting, especially when the cluster is already live. Recently, I needed to migrate my cluster from the `192.168.1.0/24` subnet to `10.0.10.0/24`. This post documents the steps I followed to update my three-node cluster without breaking it.

## Planning the Migration

Before diving in, it’s essential to prepare:

1. **Backup Configurations**: Always back up configuration files in case you need to roll back changes.
1. **Check Cluster Health**: Use `pvecm status` to verify that the cluster is healthy and operating normally.
1. **Define New IPs**:
   ```plaintext
   10.0.10.1        - Gateway
   255.255.255.0    - Netmask
   10.0.10.51       - TATOOINE (The main node)
   10.0.10.52       - VERUNA (second node)
   10.0.10.53       - NABOO (third node)
1. **Order of Updates**: Plan which node to update first to minimize disruptions.

## Making the Changes
Here are the key files to edit, along with the commands to directly open these:

### `nano /etc/network/interfaces` - On every node!
Update the network configuration for each node. For example, on TATOOINE:

```plaintext
auto eth0
iface eth0 inet static
    address 10.0.10.51/24
    gateway 10.0.10.1
```
> Note that if you append the CIDR /24 to the address, you don't need to supply the Subnet mask in your config

### `nano /etc/hosts` - On every node!
Update the host entries to reflect the new IP addresses:

```plaintext
10.0.10.51 TATOOINE.mydomain TATOOINE
10.0.10.52 VERUNA.mydomain VERUNA
10.0.10.53 NABOO.mydomain NABOO
```

### `nano /etc/pve/corosync.conf`
Modify the ring0_addr values in Corosync configuration:

```plaintext
nodelist {
    node {
        name: TATOOINE
        nodeid: 1
        quorum_votes: 1
        ring0_addr: 10.0.10.51
    }
    node {
        name: VERUNA
        nodeid: 2
        quorum_votes: 1
        ring0_addr: 10.0.10.52
    }
    node {
        name: NABOO
        nodeid: 3
        quorum_votes: 1
        ring0_addr: 10.0.10.53
    }
}
```

### `nano /etc/resolv.conf` - On every node!
Update /etc/resolv.conf for DNS settings if necessary.
```plaintext
search mydomain
nameserver 10.0.10.1
```

### `nano /etc/issue` - On every node!
Edit /etc/issue to reflect the new network details for banner consistency.

```plaintext
------------------------------------------------------------------------------

Welcome to the Proxmox Virtual Environment. Please use your web browser to
configure this server - connect to:

  https://10.0.10.51:8006/

------------------------------------------------------------------------------
```

## Applying the Changes
After making the changes, restart relevant services:

```bash
systemctl restart networking
systemctl restart corosync
systemctl restart pve-cluster
systemctl restart pvebanner
```
Reboot the nodes if needed.

## Testing and Verification
Once the changes are applied, verify the setup:

1. Network Connectivity: Use `ping` to ensure the nodes can reach each other.
    ```bash
    ping TATOOINE
    ping VERUNA
    ping NABOO
    ```
1. Cluster Health: Run:
    ```bash
    pvecm status
    ```
    Check that all nodes are in the cluster and communicating.
1. Logs: Review logs to catch any potential issues:
    ```bash
    journalctl -xe
    ```

## Final Thoughts
Migrating a Proxmox cluster to a new subnet can be done without significant downtime if approached methodically. The new network has improved scalability and organization in my setup.

> What challenges have you faced with Proxmox networking? Let me know in the comments!