var http = require('http');
var express = require('express');
var config = require('./config');
var app = express();
var database = require('./database');
var bodyParser = require('body-parser');
//app.use(express['static'](__dirname));
app.use(bodyParser.urlencoded());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: true
}));

app.get('/items/:id', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.findItem(req.params.id).then(function (result) {
        res.status(200).send(result);
    });
});

app.post('/items/:id', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}\n Body:  ${JSON.stringify(req.body)}`);
    database.updateItem(req.params.id, req.body).then(function (result) {
        res.status(200).send(result);
    });
});

app.get('/items/', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.getAllItems().then(function (result) {
        res.status(200).send(result);
    });
});

app.get('/lists/', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.getAllLists().then(function (result) {
        res.status(200).send(result);
    });
});

app.get('/lists/:id', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.findList(req.params.id).then(function (result) {
        res.status(200).send(result);
    });
});

app.post('/lists/new', function (req, res) {
    console.log(`Otrzymałem request. Body:  ${JSON.stringify(req.body)}`);
    database.saveNewList(req.body).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.post('/lists/commit/:id', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.commitList(req.params.id).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.post('/lists/terminate/:id', function (req, res) {
    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`);
    database.terminateList(req.params.id).then(
        function (result) {
            res.status(200).send(result);
        });
});



app.get('*', function (req, res) {
    res.status(404).send('Unrecogniset API call');
});

app.use(function (err, req, res, next) {
    if (req.xhr) {
        res.status(500).send('Oops, Something went wrong!');
    } else {
        next(err);
    }
});

app.listen(config.apiPort);

console.log(`SSA.Server running at port ${config.apiPort}`);
