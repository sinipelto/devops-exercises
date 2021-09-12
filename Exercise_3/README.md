# Exercise 3 - AMQP-Exercise

## Perceived benefits of using topic-based communication

Using topic-based communication is better than e.g. HTTP when there are multiple different endpoints (or topics) that
need to be discussed about and it would be too complex to build multiple HTTP APIs for example, since it would require to send
the information to all of the interest parties separately, but it would al,so require knowning these parties beforehand, which might
be in many cases be impossible or at least cost a lot to build a system like that based on HTTP.

Thus, using a system like AMQP (Rabbit), the system benefits from both the topic system: it is possible to share information with single call to multiple parties, without requiring to know who is receiving this information, although possible if needed, and also the queuing system as a man-in-the-middle, broker, to collect multiple messages, buffer them and allow the interested parties to collect the pieces of information at their own pace - without overloading or missing them because of message flooding. Also, if the receiving system(s) went down for some reason, this queuing system allows them to catch up with the queue without losing anything or requiring the sending party to wait them until they are back up, thus not slowing the whole system down, only those parties that directly depend on them. 

## Main learnings

In this exercise, I learned quite a lot. For example, I learned building a HTTP API with C++ which I've never done before. I also learned basics of the RabbitMQ topic system, with queues, using many languages and frameworks, like Python, C# (.NET), and Javascript (Node.JS). I learnt how to fluently build systems that communicate with each other using the same interface, by sending and receiving messages through it. I learned how to build message queues, and send and receive messages with specific topics with the queues. Additionally, I learned how to use shared volumes in different ways with docker-compose, between the docker containers. I learned to build different docker images that I haven't built before, and I learned to optimize these images for example using smaller base images (buster-slim and alpine instead of the typical image) and minimize operations during the container builds.