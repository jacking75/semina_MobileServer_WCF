using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace APILogicLib.DB
{
    class UserGame
    {
        public static async Task<bool> Create(string userID, string name)
        {
            var collection = Mongo.GetGameDBCollection<DBUserBasicGameData>("BasicData");

            var basicData = new DBUserBasicGameData()
            {
                _id = userID,
                Name = name,
                UsablePosList = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false },
            };

            var result = await Task.Run(() => collection.Insert<DBUserBasicGameData>(basicData));

            return result.Ok;
        }

        public static async Task<DBUserBasicGameData> GetTutorialCheckData(string userID)
        {
            var returnValue = new DBUserBasicGameData();

            var collection = Mongo.GetGameDBCollection<BsonDocument>("BasicData");

            var query = Query.EQ("_id", userID);
            var fields = Fields.Include("Name").Include("UseBPosList");

            var dataList = await Task.Run(() => collection.Find(query).SetFields(fields));
            if (dataList.Count() != 1)
            {
                return returnValue;
            }

            foreach (var data in dataList)
            {
                returnValue.Name = data["Name"].AsString;
                returnValue.UsablePosList = data["UsablePosList"].AsBsonArray.Select(p => p.AsBoolean).ToList();
            }

            return returnValue;
        }


        public static async Task<Int64> UpdateMoneyUsablePosList(string userID, 
                                                            Int64 incDeIncMoney,
                                                            List<bool> posList)
        {
            var collection = Mongo.GetGameDBCollection<BsonDocument>("BasicData");

            var modifyArgs = new MongoDB.Driver.FindAndModifyArgs()
            {
                Query = Query.EQ("_id", userID),
                Update = Update.Inc("Money", incDeIncMoney).Set("UsablePosList", new BsonArray(posList)),
                Fields = Fields.Include("Money"),
                SortBy = SortBy.Null,
                VersionReturned = MongoDB.Driver.FindAndModifyDocumentVersion.Modified,
                Upsert = false,
            };

            var newResult = await Task.Run(() => collection.FindAndModify(modifyArgs));

            if (newResult.ModifiedDocument == null)
            {
                return Int64.MaxValue;
            }

            return newResult.ModifiedDocument["Money"].AsInt64;
        }



    }
}
