using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RESTfulAPILib.Data;

namespace RESTfulAPILib.Request
{
    public static class Login
    {
        public static RES_LOGIN Process(REQ_LOGIN requestPacket)
        {
            var responseResult = new RES_LOGIN();

            var userObject = UserRepository.GetUser(requestPacket.UserID);
            if (userObject == null)
            {
                return responseResult.Return(ERROR_CODE.REQ_LOGIN_INVALID_USER);
            }

            if (userObject.PW != requestPacket.PW)
            {
                return responseResult.Return(ERROR_CODE.REQ_LOGIN_PW);
            }

            var authToken = DateTime.Now.Ticks.ToString();
            AuthTokenRepository.Add(requestPacket.UserID, authToken);
            
            responseResult.SetResult(ERROR_CODE.NONE);
            responseResult.AuthToken = authToken;
            return responseResult;
        }
    }
}
