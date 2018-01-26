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
<<<<<<< HEAD


=======
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        }, { tableName: 'ItemModel' })
    },

    ListModel: function () {
<<<<<<< HEAD
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
=======
        return this.db.define('ListModel', {
        }, { tableName: 'ListModel' })
    },

    ItemInLists: function () {
        return this.db.define('ItemInLists', {
        }, { tableName: 'ItemInLists' })
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
    },

    ItemStatus: function () {
        return this.db.define('ItemStatus', {
        }, { tableName: 'ItemStatus' })
    },

    ListStatus: function () {
        return this.db.define('ListStatus', {
        }, { tableName: 'ListStatus' })
    },


<<<<<<< HEAD
    findItem: function (id) {
        return this.ItemModel().findOne({
            where: { ItemId: id },
            attributes: ['ItemId', 'Name', 'Description', 'KodEAN', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId']
=======
    findItem: function(id) {
        return this.ItemModel().findOne({
            where: { ItemId: id },
            attributes: ['ItemId', 'Name', 'Description']
>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
        });
    },

    getAllItems: function () {
        return this.ItemModel().findAll({
<<<<<<< HEAD
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

=======
            attributes: ['ItemId', 'Name', 'Description']
        });
    },

>>>>>>> 154b55bd9b64ec661a2dc3029f209795697e6681
};
module.exports = database;
