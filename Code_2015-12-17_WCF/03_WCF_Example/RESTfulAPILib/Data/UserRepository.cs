using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

using MongoLib = RESTfulAPILib.DB.MongoDBLib;

namespace RESTfulAPILib.Data
{
    public static class UserRepository
    {
        public static async Task AddUserAsync(string userID, string pw)
        {
            var user = new DB.DBUser()
             {
                 UID = DateTime.Now.Ticks,
                 _id = userID,
                 PW = pw,
             };

            var collection = MongoLib.GetAccountDBUserCollection<DB.DBUser>();
            await collection.InsertOneAsync(user);
        }

        public static async Task<DB.DBUser> GetUserAsync(string userID)
        {
            var collection = MongoLib.GetAccountDBUserCollection<DB.DBUser>();
            var data = await collection.Find(x => x._id == userID).FirstOrDefaultAsync();
            return data;
        }
    }
        
}
