using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace TestProject.MongoDBLib
{
    public static partial class UserGameData
    {
        public static async Task<bool> InsertItem(string userID, List<int> ItemIDList)
        {
            try
            {
                var newItemList = new List<DBUserItem>();

                foreach (var itemID in ItemIDList)
                {
                    var newData = new DBUserItem()
                    {
                        _id = UniqueSeqNumberGenerator.채번_받아오기(),
                        UserID = userID,
                        ItemID = itemID,
                        AcquireDateTime = DateTime.Now,
                    };

                    newItemList.Add(newData);
                }

                var collection = Common.GetDBCollection<DBUserItem>("Item");
                await collection.InsertManyAsync(newItemList);
                return true;
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message, LOG_LEVEL.ERROR);
                return false;
            }
        }
    }



    public class DBUserItem
    {
        public Int64 _id; // Unique ID

        public string UserID; 

        public int ItemID;

        [MongoDB.Bson.Serialization.Attributes.BsonElement("AD")]
        public DateTime AcquireDateTime; // 아이템 입수 시간
    }
}
