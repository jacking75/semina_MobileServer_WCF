using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver.Core.Misc;
using MongoDB.Driver;

namespace TestProject.MongoDBLib
{
    // https://www.mongodb.com/blog/post/introducing-20-net-driver
    // http://mongodb.github.io/mongo-csharp-driver/2.0/reference/
    // http://api.mongodb.org/csharp/2.0/html/R_Project_CSharpDriverDocs.htm
    public static class Common
    {
        static string DBConnectString;
        static string DBName;


        public static void SetDBInfo(string connect, string dbName)
        {
            DBConnectString = connect;
            DBName = dbName;
        }

        public static IMongoCollection<T> GetDBCollection<T>(string collectionName)
        {
            var mongoClient = GetDBClient(DBConnectString);
            if (mongoClient == null)
            {
                return null;
            }

            var collection = mongoClient.GetDatabase(DBName).GetCollection<T>(collectionName);
            return collection;
        }


        static IMongoClient GetDBClient(string connectString)
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
                DevLog.Write(ex.Message);
                return null;
            }
        }
    }
}
