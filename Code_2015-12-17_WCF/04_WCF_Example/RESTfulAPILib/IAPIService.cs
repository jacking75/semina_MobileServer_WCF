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
        RES_ENCRYTO_DATA TestEncrypto(REQ_ENCRYTO_DATA requestPacket);


        [OperationContract]
        Task<RES_ENCRYTO_DATA> RequestCreateUser(REQ_ENCRYTO_DATA requestPacket);

        [OperationContract]
        Task<RES_ENCRYTO_DATA> RequestLogin(REQ_ENCRYTO_DATA requestPacket);

        [OperationContract]
        Task<RES_ENCRYTO_DATA> RequestLoadBasicGameData(REQ_ENCRYTO_DATA requestPacket);
             
    }
}
