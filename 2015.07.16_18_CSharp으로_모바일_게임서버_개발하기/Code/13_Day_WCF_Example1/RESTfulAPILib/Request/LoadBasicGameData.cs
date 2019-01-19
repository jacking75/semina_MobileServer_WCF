using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RESTfulAPILib.Data;

namespace RESTfulAPILib.Request
{
    public static class LoadBasicGameData
    {
        public static async Task<RES_LOAD_BASIC_GAME_DATA> Process(REQ_LOAD_BASIC_GAME_DATA requestPacket)
        {
            var responseResult = new RES_LOAD_BASIC_GAME_DATA();

            var checkResult = await AuthTokenRepository.CheckAsync(requestPacket.UserID, requestPacket.AuthToken);
            if (checkResult == false)
            {
                return responseResult.Return(ERROR_CODE.REQ_LOAD_BASIC_GAME_DATA_INVALID_AUTH);
            }

            using (var reqLock = new RequestLock(requestPacket.UserID))
            {
                var error = await reqLock.요청_처리중인가Async();
                if (error != ERROR_CODE.NONE)
                {
                    return responseResult.Return(error);
                }

                var gameData = await BasicGameRepository.GetGameDataAsync(requestPacket.UserID);
                if (gameData == null)
                {
                    return responseResult.Return(ERROR_CODE.REQ_LOAD_BASIC_GAME_DATA_INVALID_ID);
                }

                responseResult.SetResult(ERROR_CODE.NONE);
                responseResult.Level = gameData.Level;
                responseResult.Money = gameData.Money.ToString();
            }
            
            return responseResult;
        }


        
    }
}
