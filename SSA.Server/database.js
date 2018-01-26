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
        return this.db.define('ListModel',
            {
                ListId: Sequelize.INTEGER,
                Name: Sequelize.STRING,
                Description: Sequelize.STRING,
                PersonId: Sequelize.STRING,
                CreateDate: Sequelize.STRING,
                ListStatusId: Sequelize.INTEGER
            },
            { tableName: 'ListModel' });
    },

    ItemStatus: function () {
        return this.db.define('ItemStatus', {
        }, { tableName: 'ItemStatus' })
    },

    ListStatus: function () {
        return this.db.define('ListStatus', {
        }, { tableName: 'ListStatus' })
    },

    findItem: function (id) {
        return this.ItemModel().findOne({
            where: { ItemId: id },
            attributes: ['ItemId', 'Name', 'Description', 'KodEAN', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId']
        });
    },

    getAllItems: function () {
        return this.ItemModel().findAll({
            attributes: ['ItemId', 'Name', 'Description', 'KodEAN', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId']
        });
    },

    findList: function (id) {
        return this.ListModel().findOne({
            where: { ListId: id },
            attributes: ['ListId', 'Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId']
        });
    },

    getAllLists: function () {
        return this.ListModel().findAll({
            attributes: ['ListId', 'Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId']
        });
    },

    saveNewList: function (value) {
        return this.db.query(
            `Insert Into 'ListModel' ('Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId') 
             Values ('${value.Name}','${value.Description}','${value.PersonId}','${value.CreateDate}','${value.ListStatusId}') `);
        //return this.ListModel().create(value);
    }
};
module.exports = database;
