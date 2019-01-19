using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DBLib
{
    public class MongoDBLib
    {
        public static bool Auth(string id, string pw)
        {
            var database = MongoDB.GetDataBase("test");
            var Accounts = database.GetCollection<BsonDocument>("Account");

            IMongoQuery query = Query.EQ("ID", id);
            var result = Accounts.Find(query).SingleOrDefault();

            if (result == null)
            {
                return false;
            }

            if (result["PW"] != pw)
            {
                return false;
            }

            return true;
        }

        public static void fdfd()
        {
        }
    }

    class MongoDB
    {
        public static MongoDatabase GetDataBase(string DBName)
        {
            MongoClient cli = new MongoClient("mongodb://172.20.60.221");
            MongoDatabase testdb = cli.GetServer().GetDatabase(DBName);

            return testdb;
        }
    }
}
