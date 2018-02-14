var http = require('http');
var express = require('express');
var config = require('./config');
var app = express();
var database = require('./database');
var bodyParser = require('body-parser');
var url = require('url');

var printRequest = function(req)
{
    console.log(`${req.method} na ${req.originalUrl}\nParametry: ${JSON.stringify(req.params)}\tBody: ${JSON.stringify(req.body, null, 4)}`);
}


app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: true
}));

app.get('/items/', function (req, res) {
    printRequest(req);
    database.getAllItems().then(function (result) {
        res.status(200).send(result);
    });
});

app.get('/items/:id', function (req, res) {
    printRequest(req);
    database.findItem(req.params.id).then(function (result) {
        res.status(200).send(result);
    });
});

app.put('/items/:id', function (req, res) {
    printRequest(req);
    database.updateItem(req.params.id, req.body).then(function (result) {
        res.status(200).send(result);
    });
});

//app.post('/items/:id/addToList/:listId', function (req, res) {
//    console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}\n Params:  ${JSON.stringify(req.params)}`);
//    database.addItemToList(req.params.id, req.params.listId).then(function (result) {
//        res.status(200).send(result);
//    });
//});


app.get('/lists/', function (req, res) {
    printRequest(req);
    database.getAllLists().then(function (result) {
        res.status(200).send(result);
    });
});

app.get('/lists/:id', function (req, res) {
    printRequest(req);
    database.findList(req.params.id).then(function (result) {
        res.status(200).send(result);
    });
});

app.post('/lists/new', function (req, res) {
    printRequest(req);
    database.saveNewList(req.body).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.put('/lists/:id', function (req, res) {
    printRequest(req);
    database.updateList(req.params.id, req.body).then(function (result) {
        res.status(200).send(result);
    });
});

app.post('/lists/commit/:id', function (req, res) {
    printRequest(req);
    database.commitList(req.params.id).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.post('/lists/terminate/:id', function (req, res) {
    printRequest(req);
    database.terminateList(req.params.id).then(
        function (result) {
            res.status(200).send(result);
        });
});


app.get('/persons/', function (req, res) {
    printRequest(req);
    database.findAllPersons().then(
        function (result) {
            res.status(200).send(result);
        });
});

app.get('/persons/:id', function (req, res) {
    printRequest(req);
    database.findPerson(req.params.id).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.post('/persons/new', function (req, res) {
    printRequest(req);
    database.saveNewPerson(req.body).then(
        function (result) {
            res.status(200).send(result);
        });
});

app.get('*', function (req, res) {
    printRequest(req);
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
