using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib.Data
{
    // 유저 저장소
    public static class UserRepository
    {
        static List<User> Users = new List<User>();
                

        public static void AddUser(string id, string pw)
        {
             var user = new User()
             {
                 UID = DateTime.Now.Ticks,
                 ID = id,
                 PW = pw,
             };
            
             Users.Add(user);
        }

        public static User GetUser(string id)
        {
            return Users.Find(data => data.ID == id);
        }
    }

    public class User
    {
        public Int64 UID; // Unique Index
        public string ID;
        public string PW;
    }
}
