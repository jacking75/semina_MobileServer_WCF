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

namespace TestProject
{
    public static class MongoDBLib1
    {
        static string DBConnectString;
        static string DBName;
                        

        public static void SetDBInfo(string connect, string dbName)
        {
            DBConnectString = connect;
            DBName = dbName;
        }
                
        public static MongoCollection<T> GetDBCollection<T>(string collectionName)
        {
            var mongoClient = GetDBClient(DBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var database = mongoClient.GetServer().GetDatabase(DBName);
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
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return null;
            }
        }
    }
}
