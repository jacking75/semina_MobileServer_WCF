using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace RESTfulAPILib.DB
{
    public static class MongoDBLib
    {
        const string ConnectString = "mongodb://localhost:27017/?maxPoolSize=200";
        
        const string ACCOUNT_DATABASE = "TestAccountDB";
        const string USER_DATA_COLLECTION = "Users";

        const string GAME_DATABASE = "TestGameDB";
        const string USER_BASIC_GAME_DATA_COLLECTION = "UserBasicData";



        public static MongoCollection<T> GetAccountDBUserCollection<T>()
        {
            return GetDBCollection<T>(ACCOUNT_DATABASE, USER_DATA_COLLECTION);
        }

        public static MongoCollection<T> GetGameDBUserBasicDataCollection<T>()
        {
            return GetDBCollection<T>(GAME_DATABASE, USER_BASIC_GAME_DATA_COLLECTION);
        }

        static MongoCollection<T> GetDBCollection<T>(string dbName, string collectionName)
        {
            var database = GetMongoDatabase(dbName);
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }

        static MongoDatabase GetMongoDatabase(string dbName)
        {
            MongoClient cli = new MongoClient(ConnectString);
            MongoDatabase database = cli.GetServer().GetDatabase(dbName);
            return database;
        }
    }


    public class DBUser
    {
        public string _id; // User ID
        public Int64 UID; // Unique Index        
        public string PW;
    }

    public class DBBasicGameData
    {
        public string _id; // User ID
        public int Level;
        public Int64 Money;
    }
}
