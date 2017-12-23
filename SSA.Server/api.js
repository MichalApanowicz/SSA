var http = require('http');
var express = require('express');
var config = require('./config');
var app = express();
var database = require('./database');

//app.use(express['static'](__dirname));

app.get('/items/:id', function(req, res){
	console.log(`Otrzymałem request. Parametry:  ${JSON.stringify(req.params)}`)
	res.status(200).send(database.findItem(req.params.id));
});

app.get('*', function(req, res) {
	res.status(404).send('Unrecogniset API call');
});

app.use(function(err, req, res, next){
	if(req.xhr) {
		res.status(500).send('Oops, Something went wrong!');
	} else {
		next(err);
	}
});

app.listen(config.apiPort);

console.log(`SSA.Server running at port ${config.apiPort}`);
