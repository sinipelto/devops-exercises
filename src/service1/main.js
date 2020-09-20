const express = require('express');
const request = require('request');

const app = express();
const port = 81;
const dest = 82;

app.get('/', (req, res) => {
    console.log("Received GET 1");
    console.log("Sending GET 2");

    request.get("http://localhost:" + dest + "/")
        .on('response', (resp) => {
            console.log("Response OK from GET 2");
            let data = "";
            resp.on('data', (chunk) => {
                console.log("Received buffer data");
                data += chunk;
            });
            resp.on('end', () => {
                console.log("Data stream finished.");

                let final = "SERVICE 1: " + "<br>"
                    + 'Hello from ' + req.client.remoteAddress + ":" + req.client.remotePort
                    + "<br>"
                    + " sent to " + req.client.localAddress + ":" + req.client.localPort;

                final += "<br>";
                final += "SERVICE 2: " + "<br>" + data;

                res.send(final);
            });
        })
        .on('error', (err) => {
            console.log("Response ERROR from GET 2: " + err);
            res.send("Sub-request failed with error: " + err);
        });
});

app.listen(port, () => {
    console.log(`Service 1 running at http://localhost:${port}`)
});
