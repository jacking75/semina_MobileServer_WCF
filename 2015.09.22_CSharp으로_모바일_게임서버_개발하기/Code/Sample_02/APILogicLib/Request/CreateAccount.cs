using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib.Request
{
    public static class CreateAccount
    {
        public static RES_CREATE_ACCOUNT Process(REQ_CREATE_ACCOUNT requestPacket)
        {
            var responseResult = new RES_CREATE_ACCOUNT();

            var result = DB.UserAccount.CreateAsync(requestPacket.ID, requestPacket.PW);
            if (result.Result != ERROR_ID.NONE)
            {
                responseResult.Result = (short)result.Result;
                return responseResult;
            }

            responseResult.Result = (short)ERROR_ID.NONE;
            return responseResult;
        }
    }
}
