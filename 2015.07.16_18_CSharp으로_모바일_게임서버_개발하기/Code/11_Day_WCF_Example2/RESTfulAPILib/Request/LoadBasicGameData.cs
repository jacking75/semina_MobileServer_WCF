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
        public static RES_LOAD_BASIC_GAME_DATA Process(REQ_LOAD_BASIC_GAME_DATA requestPacket)
        {
            var responseResult = new RES_LOAD_BASIC_GAME_DATA();

            if (AuthTokenRepository.Check(requestPacket.UserID, requestPacket.AuthToken) == false)
            {
                responseResult.Return(ERROR_CODE.REQ_LOAD_BASIC_GAME_DATA_INVALID_AUTH);
            }

            var gameData = BasicGameRepository.GetGameData(requestPacket.UserID);
            if (gameData == null)
            {
                responseResult.Return(ERROR_CODE.REQ_LOAD_BASIC_GAME_DATA_INVALID_ID);
            }
                        

            responseResult.SetResult(ERROR_CODE.NONE);
            responseResult.Level = gameData.Level;
            responseResult.Money = gameData.Money.ToString();
            return responseResult;
        }
    }
}
