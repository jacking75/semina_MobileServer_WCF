using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver.Builders;

using MongoLib = RESTfulAPILib.DB.MongoDBLib;

namespace RESTfulAPILib.Data
{
    public static class UserRepository
    {
        public static void AddUser(string userID, string pw)
        {
            var user = new DB.DBUser()
             {
                 UID = DateTime.Now.Ticks,
                 _id = userID,
                 PW = pw,
             };

            var collection = MongoLib.GetAccountDBUserCollection<DB.DBUser>();
            collection.Save(user);
        }

        public static DB.DBUser GetUser(string userID)
        {
            var collection = MongoLib.GetAccountDBUserCollection<DB.DBUser>();
            var data = collection.FindOne(Query.EQ("_id", userID));
            return data;
        }
    }
        
}
