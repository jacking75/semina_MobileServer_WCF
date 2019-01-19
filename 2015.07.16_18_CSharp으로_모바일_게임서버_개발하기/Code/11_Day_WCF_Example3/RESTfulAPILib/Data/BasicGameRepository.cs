using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver.Builders;

using MongoLib = RESTfulAPILib.DB.MongoDBLib;

namespace RESTfulAPILib.Data
{
    public static class BasicGameRepository
    {
        public static void AddGameData(string userID)
        {
            var rand = new Random();

            var gameData = new DB.DBBasicGameData()
            {
                _id = userID,
                Level = rand.Next(1, 11),
                Money = rand.Next(1000, 100000),
            };

            var collection = MongoLib.GetGameDBUserBasicDataCollection<DB.DBBasicGameData>();
            collection.Save(gameData);
        }

        public static DB.DBBasicGameData GetGameData(string userID)
        {
            var collection = MongoLib.GetGameDBUserBasicDataCollection<DB.DBBasicGameData>();
            var data = collection.FindOne(Query.EQ("_id", userID));
            return data;
        }
    }
       
}
