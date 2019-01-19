using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib.Data
{
    public static class AuthTokenRepository
    {
        static Dictionary<string, string> AuthTokenDic = new Dictionary<string, string>();


        public static void Add(string userID, string authToken)
        {
            DB.Redis.SetStringNoReturn<DBUserSession>(userID, new DBUserSession() { AuthToken = authToken, CV = 1, CDV = 1 });
        }

        public static bool Check(string userID, string authToken)
        {
            var sessionInfo = DB.Redis.GetString<DBUserSession>(userID);
            if (sessionInfo.Item1 == false || sessionInfo.Item2.AuthToken != authToken)
            {
                return false;
            }

            return true;
        }
               
    }

    public struct DBUserSession
    {
        public string AuthToken;
        public short CV;            // ClientVersion
        public short CDV;           // ClientDataVersion
    }
}
