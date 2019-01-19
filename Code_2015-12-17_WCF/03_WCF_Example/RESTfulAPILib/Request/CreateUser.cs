using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RESTfulAPILib.Data;

namespace RESTfulAPILib.Request
{
    public static class CreateUser
    {
        public static async Task<RES_CREATE_USER> Process(REQ_CREATE_USER requestPacket)
        {
            var responseResult = new RES_CREATE_USER();

            if (string.IsNullOrEmpty(requestPacket.UserID))
            {
                return responseResult.Return(ERROR_CODE.REQ_CREATE_USER_INVALID_ID);
            }

            if (UserRepository.GetUserAsync(requestPacket.UserID) != null)
            {
                return responseResult.Return(ERROR_CODE.REQ_CREATE_USER_DUPLICATE_USER_ID);
            }

            await UserRepository.AddUserAsync(requestPacket.UserID, requestPacket.PW);
            await BasicGameRepository.AddGameDataAsync(requestPacket.UserID);

            responseResult.SetResult(ERROR_CODE.NONE);
            return responseResult;
        }
    }
}
