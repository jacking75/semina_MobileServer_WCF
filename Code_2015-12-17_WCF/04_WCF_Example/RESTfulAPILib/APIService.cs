using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel.Web;
using System.ServiceModel;


namespace RESTfulAPILib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class APIService : IAPIService
    {
        APIService()
        {
            Main.Init();
        }


        // http://localhost:23333/APIService/TestEncrypto
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "TestEncrypto")]
        public RES_ENCRYTO_DATA TestEncrypto(REQ_ENCRYTO_DATA requestPacket)
        {
            try
            {
                var jsonObject = DecryptRequestData<REQ_DEV_ECHO>(requestPacket.Data);
                var result = new RES_DEV_ECHO() { Result = true, ResData = jsonObject.ReqData };
                return EncryotResponseData<RES_DEV_ECHO>(result);
            }
            catch (Exception ex)
            {
                var result = new RES_ENCRYTO_DATA() { Result = (short)ERROR_CODE.EXCEPTION, Data = ex.Message };
                return result;
            }
        }


        #region RequestCreateUser
        // http://localhost:23333/APIService/RequestCreateUser
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestCreateUser")]
        public async Task<RES_ENCRYTO_DATA> RequestCreateUser(REQ_ENCRYTO_DATA requestPacket)
        {
            var jsonObject = DecryptRequestData<REQ_CREATE_USER>(requestPacket.Data);
            var result = await Request.CreateUser.Process(jsonObject);
            return EncryotResponseData<RES_CREATE_USER>(result);
        }
        #endregion


        #region RequestLogin
        // http://localhost:23333/APIService/RequestLogin
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestLogin")]
        public async Task<RES_ENCRYTO_DATA> RequestLogin(REQ_ENCRYTO_DATA requestPacket)
        {
            var jsonObject = DecryptRequestData<REQ_LOGIN>(requestPacket.Data);
            var result = await Request.Login.Process(jsonObject);
            return EncryotResponseData<RES_LOGIN>(result);
        }
        #endregion


        #region RequestLoadBasicGameData
        // http://localhost:23333/APIService/RequestLoadBasicGameData
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestLoadBasicGameData")]
        public async Task<RES_ENCRYTO_DATA> RequestLoadBasicGameData(REQ_ENCRYTO_DATA requestPacket)
        {
            var jsonObject = DecryptRequestData<REQ_LOAD_BASIC_GAME_DATA>(requestPacket.Data);
            var result = await Request.LoadBasicGameData.Process(jsonObject);
            return EncryotResponseData<RES_LOAD_BASIC_GAME_DATA>(result);
        }
        #endregion



        //[[ 관리 기능 관련 ]]
        // AWS의 ELB에서 서버가 살아 있는지 조사할 때 사용. 일종의 Heartbeat
        // http://localhost:23333/APIService/RequestHeathCheck
        [WebInvoke(Method = "GET",
                    UriTemplate = "RequestHeathCheck")]
        public void RequestHeathCheck()
        {
            var remoteIP = RemoteClientIP();

            if (Main.EnableRequestHeathCheck)
            {
                Console.WriteLine("RequestHeathCheck");
            }
            else
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                response.StatusDescription = "Server Spot Check";
            }
        }

        string RemoteClientIP()
        {
            var context = OperationContext.Current;
            var mp = context.IncomingMessageProperties;
            var propName = System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name;
            var prop = (System.ServiceModel.Channels.RemoteEndpointMessageProperty)mp[propName];
            string remoteIP = prop.Address;
            return remoteIP;
        }

        REQUSET_T DecryptRequestData<REQUSET_T>(string request)
        {
            try
            {
                var dynamicKey = "f&dsf";
                var decryptData = Crypto.AESEncrypt.Decrypt(dynamicKey, request);
                var jsonObject = Jil.JSON.Deserialize<REQUSET_T>(decryptData);
                return jsonObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default(REQUSET_T);
            }
        }

        RES_ENCRYTO_DATA EncryotResponseData<RESULT_T>(RESULT_T result)
        {
            var dynamicKey = "f&d23";
            var response = new RES_ENCRYTO_DATA { Result = (short)ERROR_CODE.NONE };

            var jsonResObject = Jil.JSON.Serialize<RESULT_T>(result);
            var encryData = Crypto.AESEncrypt.Encrypt(dynamicKey, jsonResObject);

            response.Result = (short)ERROR_CODE.NONE;
            response.Data = encryData;

            return response;
        }
    }


    
}
