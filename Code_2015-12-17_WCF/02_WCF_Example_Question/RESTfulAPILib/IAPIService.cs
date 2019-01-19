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
        RES_DEV_ECHO TestEcho(REQ_DEV_ECHO requestPacket);


        //TODO: 유저 생성

        //TODO: 로그인

        //TODO: 유저 기본 게임 데이터 로딩

    }
}
