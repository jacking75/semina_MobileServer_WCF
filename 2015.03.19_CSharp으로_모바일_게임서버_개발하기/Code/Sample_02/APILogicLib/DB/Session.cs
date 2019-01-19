using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib.DB
{
    public static class Session
    {
        public static async Task<string> SaveAuthInfoAsync(string id, DBUserSession userAuth)
        {
            userAuth.AuthToken = Util.GenerateSecureNumber2(1, 9, 8);

            await Redis.SetString<DBUserSession>(id, userAuth);
            return userAuth.AuthToken;
        }

        public static async Task<bool> CheckAuthInfoAsync(string id, string authToken)
        {
            var sessionInfo = await Redis.GetString<DBUserSession>(id);
            if (sessionInfo.Item1 == false || sessionInfo.Item2.AuthToken != authToken)
            {
                return false;
            }

            return true;
        }
       
    }
}
