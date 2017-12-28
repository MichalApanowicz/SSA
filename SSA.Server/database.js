var Sequelize = require('sequelize');

var database = {
    db: new Sequelize('database', 'username', 'password', {
        host: 'localhost',
        dialect: 'sqlite',

        pool: {
            max: 5,
            min: 0,
            acquire: 30000,
            idle: 10000
        },
        storage: 'Database.db'
    }),

    ItemModel: function () {
        return this.db.define('ItemModel', {
        }, { tableName: 'ItemModel' })
    },

    ListModel: function () {
        return this.db.define('ListModel', {
        }, { tableName: 'ListModel' })
    },

    ItemInLists: function () {
        return this.db.define('ItemInLists', {
        }, { tableName: 'ItemInLists' })
    },

    ItemStatus: function () {
        return this.db.define('ItemStatus', {
        }, { tableName: 'ItemStatus' })
    },

    ListStatus: function () {
        return this.db.define('ListStatus', {
        }, { tableName: 'ListStatus' })
    },


    findItem: function(id) {
        return this.ItemModel().findOne({
            where: { ItemId: id },
            attributes: ['ItemId', 'Name', 'Description']
        });
    },

    getAllItems: function () {
        return this.ItemModel().findAll({
            attributes: ['ItemId', 'Name', 'Description']
        });
    },

};
module.exports = database;
