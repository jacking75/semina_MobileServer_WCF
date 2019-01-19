using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Runtime.Serialization;
using System.ServiceModel;

using APILogicLib;


namespace RESTfulAPILib
{
    [ServiceContract]
    public interface IAPIService
    {                
        [OperationContract]
        RES_DEV_ECHO TestEcho(REQ_DEV_ECHO requestPacket);

        [OperationContract]
        RES_CREATE_ACCOUNT CreateAccount(REQ_CREATE_ACCOUNT requestPacket);

        [OperationContract]
        RES_LOG_IN LogIn(REQ_LOG_IN requestPacket);

        [OperationContract]
        RES_LOAD_USER_GAME_DATA LoadUserGameData(REQ_LOAD_USER_GAME_DATA requestPacket);

        [OperationContract]
        Task<RES_LOAD_USER_GAME_DATA> LoadUserGameData2(REQ_LOAD_USER_GAME_DATA requestPacket);
                       

        [OperationContract]
        RES_ENCRYTO_DATA TestEncryto(REQ_ENCRYTO_DATA requestPacket);


        [OperationContract]
        void RequestHeathCheck();
    }
}
