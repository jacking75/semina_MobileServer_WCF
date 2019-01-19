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


        // 유저 생성
        [OperationContract]
        RES_CREATE_USER RequestCreateUser(REQ_CREATE_USER requestPacket);

        // 로그인
        [OperationContract]
        RES_LOGIN RequestLogin(REQ_LOGIN requestPacket);

        // 유저 기본 게임 데이터 로딩
        [OperationContract]
        RES_LOAD_BASIC_GAME_DATA RequestLoadBasicGameData(REQ_LOAD_BASIC_GAME_DATA requestPacket);
             
    }
}
