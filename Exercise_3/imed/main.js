#!/usr/bin/env node

const amqp = require('amqplib/callback_api');
const waitf = require('wait-forever');

const host = process.env.APP_ENV === "PRODUCTION" ? "rabbit" : "localhost";
const exchange = "msg_topic";
const topic_receive = "my.o";
const topic_send = "my.i";

amqp.connect('amqp://' + host, function (error0, connection) {
    if (error0) {
        throw error0;
    }

    // SEND
    function sendMsg(ex, key, msg) {
        connection.createChannel(function (error1, channel) {
            if (error1) {
                throw error1;
            }

            channel.assertExchange(ex, 'topic', {
                durable: false
            });

            channel.publish(ex, key, Buffer.from(msg));

            console.log(" [x] Sent: %s:'%s'", key, msg);
        });
    }

    // RECEIVE
    connection.createChannel(function (error2, channel) {
        if (error2) {
            throw error2;
        }

        channel.assertExchange(exchange, 'topic', {
            durable: false
        });

        channel.assertQueue('', {
            exclusive: true
        }, function (error3, q) {
            if (error3) {
                throw error3;
            }

            console.log(' [*] Waiting for data. To exit press CTRL+C');

            channel.bindQueue(q.queue, exchange, topic_receive);

            channel.consume(q.queue, function (msg) {
                var msgCont = msg.content.toString();
                console.log(" [x] Received: %s:'%s'", msg.fields.routingKey, msgCont);
                setTimeout(() => sendMsg(exchange, topic_send, "Got " + msgCont), 1000);
            }, {
                noAck: true
            });
        });
    });
});

(async () => {
    await waitf();
})();
