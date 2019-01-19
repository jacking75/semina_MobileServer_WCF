using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib.Data
{
    // 인증 키 저장소
    public static class AuthTokenRepository
    {
        static Dictionary<string, string> AuthTokenDic = new Dictionary<string, string>();

        // 추가
        public static void Add(string userID, string authToken)
        {
            AuthTokenDic.Add(userID, authToken);
        }

        // 확인
        public static bool Check(string userID, string authToken)
        {
            string token = "";

            if (AuthTokenDic.TryGetValue(userID, out token) == false)
            {
                return false;
            }

            if (authToken != token)
            {
                return false;
            }

            return true;
        }
    }
}
