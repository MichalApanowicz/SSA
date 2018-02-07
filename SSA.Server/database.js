var Sequelize = require('sequelize');
const Op = Sequelize.Op;
const db = new Sequelize('database',
    'username',
    'password',
    {
        host: 'localhost',
        dialect: 'sqlite',

        pool: {
            max: 5,
            min: 0,
            acquire: 30000,
            idle: 10000
        },
        storage: 'Database.db',
        operatorsAliases: false
});

const ItemModel = function() {
    return db.define('ItemModel',
        {
            ItemId: {
                type: Sequelize.INTEGER,
                primaryKey: true,
                autoIncrement: true
            },
            Name: Sequelize.STRING,
            Description: Sequelize.STRING,
            KodEAN: Sequelize.STRING,
            ItemStatusId: Sequelize.INTEGER,
            ListId: Sequelize.INTEGER,
            CategoryId: Sequelize.INTEGER,
            LocalizationId: Sequelize.INTEGER,
            Damaged: Sequelize.INTEGER
        },
        { tableName: 'ItemModel', timestamps: false })
};

const ListModel = function() {
    return db.define('ListModel',
        {
            ListId: {
                type: Sequelize.INTEGER,
                primaryKey: true,
                autoIncrement: true
            },
            Name: Sequelize.STRING,
            Description: Sequelize.STRING,
            PersonId: Sequelize.STRING,
            CreateDate: Sequelize.STRING,
            ListStatusId: Sequelize.INTEGER
        },
        { tableName: 'ListModel', timestamps: false });
};

const ItemStatus = function() {
    return db.define('ItemStatus',
        {
        },
        { tableName: 'ItemStatus' })
};

const ListStatus = function() {
    return db.define('ListStatus',
        {
        },
        { tableName: 'ListStatus' })
};

const PersonModel = function () {
    return db.define('ItemModel',
        {
            PersonId: {
                type: Sequelize.INTEGER,
                primaryKey: true,
                autoIncrement: true
            },
            Name: Sequelize.STRING,
            Description: Sequelize.STRING
        },
        { tableName: 'PersonModel', timestamps: false })
};

var database = {
   
    findItem: function(id) {
        return ItemModel().findOne({
            where: {
                ItemId: {
                    [Op.eq]: id
                }
            },
            attributes: [
                'ItemId', 'Name', 'Description', 'KodEAN', 'Damaged', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId'
            ]
        });
    },

    getAllItems: function() {
        return ItemModel().findAll({
            attributes: [
                'ItemId', 'Name', 'Description', 'KodEAN', 'Damaged', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId'
            ]
        });
    },

    addItemToList: function(itemId, listId) {
        return ItemModel().update(
            {
                ListId: listId
            },
            {
                where: {
                    ItemId: {
                        [Op.eq]: itemId
                    } 
                }
            }
        )
    },

    findList: function(id) {
        return ListModel().findOne({
            where: {
                ListId: {
                    [Op.eq]: id
                }
            },
            attributes: ['ListId', 'Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId']
        });
    },

    getAllLists: function() {
        return ListModel().findAll({
            attributes: ['ListId', 'Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId']
        });
    },

    saveNewList: function(value) {
        return ListModel().create({
            Name: value.Name,
            Description: value.Description,
            PersonId: value.PersonId,
            CreateDate: value.CreateDate,
            ListStatusId: value.ListStatusId
        })

        //db.query(
        //    `Insert Into 'ListModel' ('Name', 'Description', 'PersonId', 'CreateDate', 'ListStatusId') 
        //     Values (:name, :description, :personId, :createDate, :listStatusId)`,
        //    {
        //        replacements:
        //        {
        //            name: value.Name,
        //            description: value.Description,
        //            personId: value.PersonId,
        //            createDate: value.CreateDate,
        //            listStatusId: value.ListStatusId
        //        }
        //    });
        //return this.ListModel().create(value);
    },

    commitList: function (id) {

        return db.transaction(function (t) {

            return ListModel().update(
                {
                    ListStatusId: 2 
                },
                {
                    where: {
                        ListId: {
                            [Op.eq]: id
                        } 
                    },
                    transaction: t 
                }
            ).then(function(val) {
                return ItemModel().update(
                    {
                        ItemStatusId: 3
                    },
                    {
                        where: {
                            ListId: {
                                [Op.eq]: id
                            },
                            ItemStatusId: {
                                [Op.eq]: 1
                            } 
                        },
                        transaction: t
                    }
                )
            }).then(function (val) {
                    return ItemModel().update(
                        {
                            ListId: 0
                        },
                        {
                            where: {
                                ListId: {
                                    [Op.eq]: id
                                },
                                [Op.or]: [{ ItemStatusId: 3 }, { ItemStatusId: 2 }]
                            },
                            transaction: t
                        }
                    )
            });
        }).then(function (result) {
            console.log("SUCCESS - Commited")
        }).catch(function (err) {
            console.log(`FAIL - Rollback: ${err}`);
        });
    },

    terminateList: function (id) {

        return db.transaction(function (t) {

            return ItemModel().findAll({
                    attributes: [
                        'ItemId', 'Name', 'Description', 'KodEAN', 'Damaged', 'ItemStatusId', 'ListId', 'CategoryId',
                        'LocalizationId'
                    ],
                    where: {
                        ListId: {
                            [Op.eq]: id
                        },
                        ItemStatusId: { [Op.or]: [3, 2] }
                    }
            }).then(function (items) {
                console.log(`items: ${items}`)
                items.forEach(function(item) {
                    if (item.ItemStatusId !== 3)
                        return;
                });

                return ItemModel().update(
                    {
                        ItemStatusId: 1
                    },
                    {
                        where: {
                            ListId: {
                                [Op.eq]: id
                            },
                            ItemStatusId: 3
                        },
                        transaction: t
                    }
                )
            }).then(function (val) {
                return ListModel().update(
                    {
                        ListStatusId: 3
                    },
                    {
                        where: {
                            ListId: {
                                [Op.eq]: id
                            },
                        },
                        transaction: t
                    }
                )
            });
        }).then(function (result) {
            console.log("SUCCESS - Commited")
        }).catch(function (err) {
            console.log(`FAIL - Rollback: ${err}`);
        });
    },

    updateItem: function (id, newItem) {
        return ItemModel().update({ Damaged: newItem.Damaged, ListId: newItem.ListId },{
            where: {
                ItemId: {
                    [Op.eq]: id
                }
            },
                attributes: [
                    'ItemId', 'Name', 'Description', 'KodEAN', 'Damaged', 'ItemStatusId', 'ListId', 'CategoryId', 'LocalizationId'
                ]
        });
    },

    findPerson: function (id) {
        var x = parseInt(id);
        if (Number.isInteger(x)) {
            var condition = {
                PersonId: x
            }
        } else {
            var condition = {
                Name: {
                    [Op.eq]: id
                }
            }
        }
        return PersonModel().findOne({
            where: condition,
            attributes: ['PersonId', 'Name', 'Description']
        });
    },

    findAllPersons: function (id) {
        return PersonModel().findAll({
            attributes: ['PersonId', 'Name', 'Description']
        });
    },
    
    saveNewPerson: function (value) {
        return PersonModel().create({
            Name: value.Name,
            Description: value.Description,
        })
    },
};
module.exports = database;
