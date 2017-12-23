var x = function() {
const Sequelize = require('sequelize');
const db = new Sequelize('database', 'username', 'password', {
	host: 'localhost',
	dialect: 'sqlite',

	pool: {
		max: 5,
		min: 0,
		acquire: 30000,
		idle: 10000
	},
	storage: 'Database.db'
});

db.authenticate()
	.then(() => {
		console.log('Uzyskano połączenie!');
	})
	.catch( err => {
		console.log('Wystąpił błąd przy autentykacji.');
	});


const ItemModel = db.define('ItemModel', {
},{ tableName: 'ItemModel'});
ItemModel.removeAttribute('id');

const ListModel = db.define('ListModel', {
}, { tableName: 'ListModel' });
ListModel.removeAttribute('id');

const ItemInLists = db.define('ItemInLists', {
}, { tableName: 'ItemInLists' });
ItemInLists.removeAttribute('id');

const ItemStatus = db.define('ItemStatus', {
}, { tableName: 'ItemStatus' });
ItemStatus.removeAttribute('id');

const ListStatus = db.define('ListStatus', {
}, { tableName: 'ListStatus' });
ListStatus.removeAttribute('id');


var findItem = function(id) {
	ItemModel.findOne({
		where: {ItemId: id},
		attributes:['ItemId','Name','Description']
		}).then(item => { return item; });
	};

}
exports.database = x;
