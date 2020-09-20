#!/bin/bash

if [[ $(whoami) != "root" ]]
then
  echo "Required to run as ROOT."
  exit 1
fi

if [[ $(ls | grep 'service') == "" ]]
then
  echo "Run this script in the 'src' directory containing the sources."
  exit 1
fi

pid1=-1
pid2=-1

function handle()
{
  echo "PID1: $pid1"
  echo "PID2: $pid2"

  kill $pid1
  kill $pid2

	sleep 1

	cd service1 || echo "Failed to change directory to ./service1" && exit 1
	node main.js &
	pid1=$!
	cd ../

	cd service2 || echo "Failed to change directory to ./service2" && exit 1
	node main.js &
	pid2=$!
	cd ../

  echo "PID1: $pid1"
  echo "PID2: $pid2"
}

trap handle INT

while true
do
	sleep 1
done
