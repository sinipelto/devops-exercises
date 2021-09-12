# Get the base Ubuntu image from Docker Hub
FROM debian:buster-slim

# Specify the working directory
WORKDIR /app

# Update apps on the base image
RUN apt-get update

# Install the Clang compiler
RUN apt-get install -y make g++ librestbed-dev

# Copy the current folder which contains C++ source code to the Docker image under /usr/src
COPY . .

# Use Clang to compile the Test.cpp source file
RUN make clean && make

# Run the output program from the previous step

CMD [ "./build/server" ]
