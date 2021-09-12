FROM alpine

WORKDIR /workdir

COPY . .

CMD cp -r ./* /shared/ && chmod 0755 /shared/*.sh
