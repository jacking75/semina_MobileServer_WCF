using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILogicLib.Request
{
    public static class LoadUserGameData
    {
        public static RES_LOAD_USER_GAME_DATA Process(REQ_LOAD_USER_GAME_DATA requestPacket)
        {
            var responseResult = new RES_LOAD_USER_GAME_DATA();

            using (var reqLock = new RequestLock(requestPacket.ID))
            {
                var error = reqLock.요청_처리중인가();
                if (error.Result != ERROR_ID.NONE)
                {
                    responseResult.Result = (short)error.Result;
                    return responseResult;
                }

                var isAuthOk = DB.Session.CheckAuthInfoAsync(requestPacket.ID, requestPacket.AuthToken);
                if (isAuthOk.Result == false)
                {
                    responseResult.Result = (short)ERROR_ID.LOAD_USER_GAME_DATA_AUTH;
                    return responseResult;
                }
            }

            
            responseResult.Result = (short)ERROR_ID.NONE;
            return responseResult;
        }


        public static async Task<RES_LOAD_USER_GAME_DATA> Process2(REQ_LOAD_USER_GAME_DATA requestPacket)
        {
            var responseResult = new RES_LOAD_USER_GAME_DATA();

            using (var reqLock = new RequestLock(requestPacket.ID))
            {
                var error = await reqLock.요청_처리중인가();
                if (error != ERROR_ID.NONE)
                {
                    responseResult.Result = (short)error;
                    return responseResult;
                }

                var isAuthOk = await DB.Session.CheckAuthInfoAsync(requestPacket.ID, requestPacket.AuthToken);
                if (isAuthOk == false)
                {
                    responseResult.Result = (short)ERROR_ID.LOAD_USER_GAME_DATA_AUTH;
                    return responseResult;
                }
            }


            responseResult.Result = (short)ERROR_ID.NONE;
            return responseResult;
        }
    }
}
