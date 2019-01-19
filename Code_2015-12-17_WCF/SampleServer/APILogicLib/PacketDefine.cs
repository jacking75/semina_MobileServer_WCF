using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 클라이언트-서버 패킷 정의
namespace APILogicLib
{
    public interface IRES_DATA
    {
        void SetResult(ERROR_ID error);
    }


    public struct REQ_ENCRYTO_DATA
    {
        public string ID;
        public string Data;
    }

    public struct RES_ENCRYTO_DATA
    {
        public short Result;
        public string Data;
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



    public struct REQ_CREATE_ACCOUNT
    {
        public string ID;
        public string PW;
    }

    public struct RES_CREATE_ACCOUNT
    {
        public short Result;
    }

    public struct REQ_LOG_IN
    {
        public string ID;
        public string PW;
    }

    public struct RES_LOG_IN
    {
        public short Result;
        public string AuthToken;
    }

    public struct REQ_LOAD_USER_GAME_DATA
    {
        public string ID;
        public string AuthToken;
    }

    public struct RES_LOAD_USER_GAME_DATA
    {
        public short Result;
    }

    //public struct REQ_DATA
    //{
    //    public string LSeq;
    //    public string ID;
    //    public string Data;
    //}

    //public interface IRES_DATA
    //{
    //    void SetResult(ERROR_CODE error);
    //}

    //public struct RES_DATA : IRES_DATA
    //{
    //    public short Result;
    //    public string Data;

    //    public void SetResult(ERROR_CODE error) { Result = (short)error; }
    //}
}
