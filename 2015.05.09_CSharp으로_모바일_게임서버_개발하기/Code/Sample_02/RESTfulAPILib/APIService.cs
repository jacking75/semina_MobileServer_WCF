using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

using APILogicLib;

namespace RESTfulAPILib
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class APIService : IAPIService
    {
        APIService()
        {
            Main.Init();
        }


        //[[ 테스트 관련 ]]
        // http://localhost:23333/APIService/TestEcho
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "TestEcho")]
        public RES_DEV_ECHO TestEcho(REQ_DEV_ECHO requestPacket)
        {
            var result = APILogicLib.Request.TestEcho.Process(requestPacket);
            return result;
        }


        //[[ 계정 생성 ]]
        // http://localhost:23333/APIService/CreateAccount
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "CreateAccount")]
        public RES_CREATE_ACCOUNT CreateAccount(REQ_CREATE_ACCOUNT requestPacket)
        {
            try
            {
                var result = APILogicLib.Request.CreateAccount.Process(requestPacket);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.Message);
                return new RES_CREATE_ACCOUNT();
            }
        }


        //[[ 로그인 ]]
        // http://localhost:23333/APIService/LogIn
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "LogIn")]
        public RES_LOG_IN LogIn(REQ_LOG_IN requestPacket)
        {
            var result = APILogicLib.Request.LogIn.Process(requestPacket);
            return result;
        }



        //[[ 유저 기본 게임 데이터 로딩 ]]
        // http://localhost:23333/APIService/LoadUserGameData
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "LoadUserGameData")]
        public RES_LOAD_USER_GAME_DATA LoadUserGameData(REQ_LOAD_USER_GAME_DATA requestPacket)
        {
            var result = APILogicLib.Request.LoadUserGameData.Process(requestPacket);
            return result;
        }

        //[[ 유저 기본 게임 데이터 로딩(비동기 버전) ]]
        // http://localhost:23333/APIService/LoadUserGameData2
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "LoadUserGameData2")]
        public async Task<RES_LOAD_USER_GAME_DATA> LoadUserGameData2(REQ_LOAD_USER_GAME_DATA requestPacket)
        {
            var result = await APILogicLib.Request.LoadUserGameData.Process2(requestPacket);
            return result;
        }


        // http://localhost:23333/APIService/TestEncryto
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "TestEncryto")]
        public RES_ENCRYTO_DATA TestEncryto(REQ_ENCRYTO_DATA requestPacket)
        {
            try
            {
                var jsonObject = DecryptRequestData<REQ_DEV_ECHO>(requestPacket.Data);
                var result = APILogicLib.Request.TestEcho.Process(jsonObject);
                return EncryotResponseData<RES_DEV_ECHO>(result);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.ToString());

                var result = new RES_ENCRYTO_DATA();
                result.Result = (short)ERROR_ID.REQUEST_PACKET_DECRYPT;
                return result;  
            }
        }

        


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
                Logger.Info("RequestHeathCheck");
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
                var decryptData = AESEncrypt.Decrypt(dynamicKey, request);
                var jsonObject = Jil.JSON.Deserialize<REQUSET_T>(decryptData);
                return jsonObject;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex.ToString());
                return default(REQUSET_T);
            }
        }

        RES_ENCRYTO_DATA EncryotResponseData<RESULT_T>(RESULT_T result)
        {
            var dynamicKey = "f&d23";
            var response = new RES_ENCRYTO_DATA { Result = (short)ERROR_ID.NONE };

            var jsonResObject = Jil.JSON.Serialize<RESULT_T>(result);
            var encryData = AESEncrypt.Encrypt(dynamicKey, jsonResObject);

            response.Result = (short)ERROR_ID.NONE;
            response.Data = encryData;

            return response;
        }

        
    }
}
