FROM python:3.9-alpine

WORKDIR /app

COPY requirements.txt .

# install dependencies
RUN pip install -r requirements.txt

# copy the content of the local src directory to the working directory
COPY . .

RUN apk add --no-cache bash

CMD [ "python3", "main.py" ]
