# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  config.vm.box = "hashicorp/bionic64"

  config.vm.network "forwarded_port", guest: 80, host: 8000
  config.vm.network "forwarded_port", guest: 81, host: 8100
  config.vm.network "forwarded_port", guest: 82, host: 8200
  config.vm.network "forwarded_port", guest: 83, host: 8300
  config.vm.network "forwarded_port", guest: 84, host: 8400
  config.vm.network "forwarded_port", guest: 85, host: 8500
  config.vm.network "forwarded_port", guest: 8001, host: 8001
  config.vm.network "forwarded_port", guest: 8080, host: 8080
  config.vm.network "forwarded_port", guest: 5672, host: 5672
  config.vm.network "forwarded_port", guest: 15672, host: 15672

  config.vm.synced_folder "./Exercise_3", "/ex3", create: true

  config.vm.provision "shell", path: "prov.sh"
end
