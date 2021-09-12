const express = require('express');

const app = express();

const port = 82;

app.get('/', (req, res) => {
    res.send('Hello from '
        + req.client.remoteAddress + ":" + req.client.remotePort + "<br>"
        + " sent to " + req.client.localAddress + ":" + req.client.localPort);
});

app.listen(port, () => {
    console.log(`Service 2 running at http://localhost:${port}`)
});
