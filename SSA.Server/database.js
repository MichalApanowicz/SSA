var Sequelize = require('sequelize');

var Database =  {
	initialized : false,
	findItem : function(id) {
		this.db = new Sequelize('database', 'username', 'password', {
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
//		console.log(`-------db----> ${JSON.stringify()}`);
		this.ItemModel = this.db.define('ItemModel', {},{ tableName: 'ItemModel'});
		console.log(`-------db----> ${JSON.stringify(this.ItemModel)}`);
		this.initialized = true;

	//		if(!this.initialized) this.init();
		console.log(`------ItemModel---------> ${this.ItemModel}`);
	
		this.ItemModel.removeAttribute('id');
		this.ItemModel.findOne({
			where: {ItemId: id},
			attributes:['ItemId','Name','Description']
			}).then(item => { return item; });
	


	},
	
	ItemModel : function() { console.log(`--------shis.db-----> ${this.db}`);return this.db.define('ItemModel', {
		},{ tableName: 'ItemModel'})},

 	ListModel : function() { return this.db.define('ListModel', {
		}, { tableName: 'ListModel' })},

	ItemInLists : function() { return this.db.define('ItemInLists', {
		}, { tableName: 'ItemInLists' })},

 	ItemStatus : function() { return this.db.define('ItemStatus', {
		}, { tableName: 'ItemStatus' })},

 	ListStatus : function() { return this.db.define('ListStatus', {
		}, { tableName: 'ListStatus' })},


	aafindItem : function(id) 
	{
		if(!this.initialized) this.init();
		console.log(`------ItemModel---------> ${this.ItemModel}`);
		this.ItemModel.findOne({
			where: {ItemId: id},
			attributes:['ItemId','Name','Description']
			}).then(item => { return item; });
		},

};
module.exports = Database;
