using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace APILogicLib.DB
{
    public static class UserAccount
    {
        const string COLLECTION_NAME = "Users";

        public static async Task<ERROR_ID> CreateAsync(string userID, string pw)
        {
            var hasUser = await CheckHasUserAsync(userID);
            if (hasUser)
            {
                return ERROR_ID.CREATE_ACCOUNT_DUPLICATE;
            }


            var uid = UniqueSeqNumberGenerator.채번_받아오기();
            string hashPW = MD5Core.GetHashString(pw);
            var authUser = new DBUserAccountInfo()
            {
                _id = userID,
                PW = hashPW,
                UID = uid,
            };

            var collection = Mongo.GetAccountDBCollection<DBUserAccountInfo>(COLLECTION_NAME);
            var result = await Task.Run(() => collection.Insert<DBUserAccountInfo>(authUser));
            if (result.Ok == false)
            {
                return ERROR_ID.CREATE_ACCOUNT_DB_FAIL;
            }

            return ERROR_ID.NONE;
        }

        public static async Task<bool> CheckUserAuthAsync(string id, string pw)
        {
            var collection = Mongo.GetAccountDBCollection<DBUserAccountInfo>(COLLECTION_NAME);
            var userInfo = await Task.Run(() => collection.FindOne(Query.EQ("_id", id)));

            if (userInfo == null)
            {
                return false;
            }

            string hashPW = MD5Core.GetHashString(pw);
            if (userInfo.PW != hashPW)
            {
                return false;
            }

            return true;
        }

        static async Task<bool> CheckHasUserAsync(string userID)
        {
            var userAuthCollection = Mongo.GetAccountDBCollection<BsonDocument>(COLLECTION_NAME);
            if (userAuthCollection == null)
            {
                return false;
            }

            try
            {
                var count = await Task.Run(() => userAuthCollection.Find(Query.EQ("_id", userID)).Count());
                if (count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return false;
            }
        }
    }
}
