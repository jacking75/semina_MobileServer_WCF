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


        #region RequestCreateUser
        // http://localhost:23333/APIService/RequestCreateUser
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestCreateUser")]
        public async Task<RES_CREATE_USER> RequestCreateUser(REQ_CREATE_USER requestPacket)
        {
            var result = await Request.CreateUser.Process(requestPacket);
            return result;
        }
        #endregion


        #region RequestLogin
        // http://localhost:23333/APIService/RequestLogin
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestLogin")]
        public async Task<RES_LOGIN> RequestLogin(REQ_LOGIN requestPacket)
        {
            var result = await Request.Login.Process(requestPacket);
            return result;
        }
        #endregion


        #region RequestLoadBasicGameData
        // http://localhost:23333/APIService/RequestLoadBasicGameData
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RequestLoadBasicGameData")]
        public async Task<RES_LOAD_BASIC_GAME_DATA> RequestLoadBasicGameData(REQ_LOAD_BASIC_GAME_DATA requestPacket)
        {
            var result = await Request.LoadBasicGameData.Process(requestPacket);
            return result;
        }
        #endregion
    }


    
}
