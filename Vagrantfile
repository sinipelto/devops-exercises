# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  config.vm.box = "hashicorp/bionic64"

  config.vm.network "forwarded_port", guest: 80, host: 8000
  config.vm.network "forwarded_port", guest: 81, host: 8100
  config.vm.network "forwarded_port", guest: 82, host: 8200
  config.vm.network "forwarded_port", guest: 8001, host: 8001

  config.vm.synced_folder "./", "/src", create: true

  config.vm.provision "shell", path: "init.sh"
end
