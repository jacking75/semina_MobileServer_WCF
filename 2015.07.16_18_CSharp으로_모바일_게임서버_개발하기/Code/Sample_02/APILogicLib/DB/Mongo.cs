using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APILogicLib.DB
{
    public static class Mongo
    {
        static string AccountDBConnectString;
        static string AccountDBName;

        static string GameDBConnectString;
        static string GameDBName;

        static string GameContentsDBConnectString;
        static string GameContentsDBName;
                

        public static void SetAccountDBInfo(string connect, string dbName)
        {
            AccountDBConnectString = connect;
            AccountDBName = dbName;
        }

        public static void SetGameDBInfo(string connect, string dbName)
        {
            GameDBConnectString = connect;
            GameDBName = dbName;
        }

        public static void SetGameContentsDBInfo(string connect, string dbName)
        {
            GameContentsDBConnectString = connect;
            GameContentsDBName = dbName;
        }

        public static MongoCollection<T> GetAccountDBCollection<T>(string collectionName)
        {
            var mongoClient = GetDBClient(AccountDBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var database = mongoClient.GetServer().GetDatabase(AccountDBName);
            var coll = database.GetCollection<T>(collectionName);

            return coll;
        }

        public static MongoCollection<T> GetGameDBCollection<T>(string collectionName)
        {
            var mongoClient = GetDBClient(GameDBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var database = mongoClient.GetServer().GetDatabase(GameDBName);
            var coll = database.GetCollection<T>(collectionName);

            return coll;
        }

        public static MongoCollection<T> GetGameContentsDBCollection<T>(string collectionName)
        {
            var mongoClient = GetDBClient(GameContentsDBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var database = mongoClient.GetServer().GetDatabase(GameContentsDBName);
            var coll = database.GetCollection<T>(collectionName);

            return coll;
        }
        

        static MongoClient GetDBClient(string connectString)
        {
            try
            {
                //connectString
                //mongodb://testID:123asd@192.168.0.1:27017/?maxPoolSize=200&safe=true
                //mongodb://192.168.0.1:27017
                MongoClient cli = new MongoClient(connectString);
                return cli;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }
    }
}
