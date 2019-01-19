using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;



namespace RESTfulAPILib
{
    [ServiceContract]
    public interface IAPIService
    {                
        [OperationContract]
        RES_CREATE_USER RequestCreateUser(REQ_CREATE_USER requestPacket);

        [OperationContract]
        RES_LOGIN RequestLogin(REQ_LOGIN requestPacket);

        [OperationContract]
        RES_LOAD_BASIC_GAME_DATA RequestLoadBasicGameData(REQ_LOAD_BASIC_GAME_DATA requestPacket);
             
    }
}
