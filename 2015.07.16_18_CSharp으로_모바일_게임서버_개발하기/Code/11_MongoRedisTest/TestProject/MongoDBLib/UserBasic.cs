using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver.Core.Misc;
using MongoDB.Driver;
using MongoDB.Bson;


namespace TestProject.MongoDBLib
{
    public static partial class UserGameData
    {
        public static async Task<bool> CreateBasicDataAsyncVer1(string userID)
        {
            try
            {
                Int64 money = 1000;
                var Costume = new List<int>(Enumerable.Repeat(0,12));
                var document = new BsonDocument { { "_id", userID }, { "Level", 1 }, { "Exp", 0 }, { "Money", money }, { "Costume", new BsonArray(Costume) } };

                var collection = Common.GetDBCollection<BsonDocument>("Basic");
                await collection.InsertOneAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return false;
            }
        }

        public static async Task<bool> CreateBasicDataAsyncVer2(string userID)
        {
            try
            {
                var newData = new DBBasic()
                {
                    _id = userID,
                    Level = 1,
                    Exp = 0,
                    Money = 1000,
                    Costume = new List<int>(Enumerable.Repeat(0, 12)),
                };

                var collection = Common.GetDBCollection<DBBasic>("Basic");
                await collection.InsertOneAsync(newData);
                return true;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return false;
            }
        }


        public static async Task<DBBasic> GetUserAsyncVer1(string userID)
        {
            try
            {
                var collection = Common.GetDBCollection<DBBasic>("Basic");

                // 기본으로는 Find 메소드는 없다. Find는 확장 메소드로 사용하고 싶다면
                //using MongoDB.Driver.Core.Misc;
                //using MongoDB.Driver;
                //을 선언해야 한다.

                // 첫 번째 값 또는 null
                var document = await collection.Find(x => x._id == userID).FirstOrDefaultAsync();
                return document;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return null;
            }
        }

        public static async Task<int> GetUserLevelAsyncVer1(string userID)
        {
            try
            {
                var collection = Common.GetDBCollection<BsonDocument>("Basic");
                var filter = new BsonDocument("_id", userID);
                var documents = await collection.Find(filter).ToListAsync();

                if (documents.Count > 0)
                {
                    return documents[0]["Level"].AsInt32;
                }

                return 0;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return 0;
            }
        }

        public static async Task<List<DBBasic>> GetUserAsyncVer1(int level)
        {
            try
            {
                var collection = Common.GetDBCollection<DBBasic>("Basic");

                // 기본으로는 Find 메소드는 없다. Find는 확장 메소드로 사용하고 싶다면
                //using MongoDB.Driver.Core.Misc;
                //using MongoDB.Driver;
                //을 선언해야 한다.
                var filter = new BsonDocument("Level", new BsonDocument("$gte", 2));
                var documents = await collection.Find(filter).ToListAsync();
                return documents;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return new List<DBBasic>();
            }
        }

        public static async Task<List<DBBasic>> GetUserAsyncVer2(int level)
        {
            try
            {
                var collection = Common.GetDBCollection<DBBasic>("Basic");
                var documents = await collection.Find(x=> x.Level >= level).ToListAsync();
                return documents;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return new List<DBBasic>();
            }
        }

       

        public static async Task<List<string>> GetUserIDAsyncVer3(int level)
        {
            try
            {
                // Builders를 사용할 때는 Collection은 BsonDocument를 사용해야 한다.

                var collection = Common.GetDBCollection<BsonDocument>("Basic");

                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Gte("Level", 2) & builder.Eq("Money", 1000);
                var documents = await collection.Find(filter).ToListAsync();

                var IDList = new List<string>();

                foreach (var document in documents)
                {
                    IDList.Add(document["_id"].AsString);
                }
                return IDList;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return new List<string>();
            }
        }
    }


    // 기본 게임 데이터
    public class DBBasic
    {
        public string _id; // 유저ID
        public int Level;
        public int Exp;
        public Int64 Money;
        public List<int> Costume; // 캐릭터 복장 아이템ID. 개수는 무조건 12
    }

    
}
