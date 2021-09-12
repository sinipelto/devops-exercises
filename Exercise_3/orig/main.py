#!/usr/bin/env python

import sys
import os
import time
import pika

try:
	app_env = os.environ["APP_ENV"]
except KeyError:
	app_env = "DEVELOPMENT"

if app_env == "PRODUCTION":
	host = "rabbit"
else:
	host = "localhost"
	
exchange = "msg_topic"
routing_key = "my.o"

connection = pika.BlockingConnection(pika.ConnectionParameters(host=host))
channel = connection.channel()

print("RabbitMQ connected.", flush=True)

channel.exchange_declare(exchange=exchange, exchange_type='topic')

print("Channel exchange declared.", flush=True)

print("Sleeping couple of seconds..", flush=True)

time.sleep(3)

for i in range(1, 4):
	time.sleep(3)

	message = "MSG_" + str(i)

	channel.basic_publish(exchange=exchange, routing_key=routing_key, body=message)

	print(" [x] Sent %r:%r" % (routing_key, message), flush=True)

connection.close()

print("Connection closed.", flush=True)
print("Operations completed.", flush=True)
