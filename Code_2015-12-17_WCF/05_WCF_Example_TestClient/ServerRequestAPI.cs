using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;


namespace TestProject
{
    /*
    static class ServerRequestAPI
    {
        public static async Task<int> RequestAPI(REQUEST_API_TYPE apiType)
        {
            var dynamicKey = "f&d23";
            var resultCode = 0;

            try
            {
                switch (apiType)
                {
                    case REQUEST_API_TYPE.LOGIN:
                        {
                            //var request = new REQ_LOGIN_DATA()
                            //{
                            //    ID = UserInfo.UserID,
                            //    PW = UserInfo.PW,
                            //    ClientVer = UserInfo.ClientVer,
                            //    DataVer = UserInfo.DataVer,
                            //    MarketType = UserInfo.MarketType,
                            //};

                            //var api = "http://" + ServerAddress + "/GameService/RequestLogin";
                            //var jsonText = JsonConvert.SerializeObject(request);
                            //var result = await RequestHttpAESEncry<RES_LOGIN_DATA>(api,
                            //                                        AESEncrypt.AesLoginDynamicKey,
                            //                                        UserInfo.UserID,
                            //                                        jsonText);

                            //resultCode = (ERROR_CODE)result.Result;
                            //UserInfo.AuthToken = result.AT;
                            //UserInfo.LoginSeq = result.LSeq;
                        }
                        break;
                                            
                }
            }
            catch (Exception ex)
            {
                return ERROR_ID.CLIENT_HTTP_NETWOR_EXCEPTION;
            }

            return resultCode;
        }

        static async Task<RESULT_T> RequestHttpAESEncry<RESULT_T>(string api,
                                                    string loginSeq,
                                                    string userID,
                                                    string jsonText) where RESULT_T : IRES_DATA, new()
        {
            var dynamicKey = "f&dsf";

            var resultData = new RESULT_T();

            var encryData = AESEncrypt.Encrypt(loginSeq, jsonText);
            var sendData = new REQ_ENCRYTO_DATA { ID = userID, Data = encryData };
            var requestJson = Jil.JSON.Serialize<REQ_ENCRYTO_DATA>(sendData);

            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(requestJson));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var network = new System.Net.Http.HttpClient();
            var response = await network.PostAsync(api, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode == false)
            {
                resultData.SetResult(ERROR_ID.CLIENT_HTTP_NETWOR_CONNECT);
                return resultData;
            }

            var responeString = await response.Content.ReadAsStringAsync();
            var responseJson = Jil.JSON.Deserialize<RES_ENCRYTO_DATA>(responeString);

            if (responseJson.Result != (short)ERROR_ID.NONE)
            {
                resultData.SetResult((ERROR_ID)responseJson.Result);
                return resultData;
            }

            var decryptData = AESEncrypt.Decrypt(dynamicKey, responseJson.Data);
            resultData = Jil.JSON.Deserialize<RESULT_T>(decryptData);
            return resultData;
        }
    }
    */
    public enum REQUEST_API_TYPE
    {
        NONE = 0,
        ECHO,
        LOGIN,
        LOAD_BASIC_GAME_DATA,
    }

    public struct REQ_DEV_ECHO
    {
        public int WaitSec;
        public string ReqData;
    }

    public struct RES_DEV_ECHO
    {
        public bool Result;
        public string ResData;
    }


    public struct REQ_ENCRYTO_DATA
    {
        public string UserID;
        public string Data;
    }

    public struct RES_ENCRYTO_DATA
    {
        public short Result;
        public string Data;
    }
}
