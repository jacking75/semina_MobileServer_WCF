using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib.Request
{
    public static class LogIn
    {
        public static RES_LOG_IN Process(REQ_LOG_IN requestPacket)
        {
            var responseResult = new RES_LOG_IN();

            var result = DB.UserAccount.CheckUserAuthAsync(requestPacket.ID, requestPacket.PW);
            if (result.Result == false)
            {
                responseResult.Result = (short)ERROR_ID.LOG_IN_PW;
                return responseResult;
            }

            var sessionInfo = new DB.DBUserSession()
            {
                CV = 1,
                CDV = 1,
            };

            var token = DB.Session.SaveAuthInfoAsync(requestPacket.ID, sessionInfo);
            
            var reqLock = new RequestLock(requestPacket.ID);
            reqLock.SetInit();

            responseResult.AuthToken = token.Result;
            responseResult.Result = (short)ERROR_ID.NONE;

            return responseResult;
        }
    }
}
