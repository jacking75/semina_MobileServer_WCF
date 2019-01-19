using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib
{
    #region REQ_DEV_ECHO
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
    #endregion


    #region REQ_CREATE_USER
    public struct REQ_CREATE_USER
    {
        public string UserID;
        public string PW;
    }

    public struct RES_CREATE_USER
    {
        public RES_CREATE_USER Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
    }
    #endregion


    #region REQ_LOGIN
    public struct REQ_LOGIN
    {
        public string UserID;
        public string PW;
    }

    public struct RES_LOGIN
    {
        public RES_LOGIN Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
        public string AuthToken;
    }
    #endregion


    #region REQ_LOAD_BASIC_GAME_DATA
    public struct REQ_LOAD_BASIC_GAME_DATA
    {
        public string UserID;
        public string AuthToken;
    }

    public struct RES_LOAD_BASIC_GAME_DATA
    {
        public RES_LOAD_BASIC_GAME_DATA Return(ERROR_CODE error)
        {
            Result = (short)error; return this;
        }

        public void SetResult(ERROR_CODE error) { Result = (short)error; }


        public short Result;
        public int Level;
        public string Money;
    }
    #endregion


    //public interface IRES_DATA
    //{
    //    void SetResult(ERROR_CODE error);
    //}

    //public struct RES_HBM_MEMBER_CHECK_DATA : IRES_DATA
    //{
    //    public void SetResult(ERROR_CODE error) { Result = (short)error; }
    //    public RES_HBM_MEMBER_CHECK_DATA Return(ERROR_CODE error)
    //    {
    //        Result = (short)error; return this;
    //    }

    //    public short Result;   // 요청에 대한 결과
    //    public string AT;       // AuthToken 게임 전용 인증 토큰
    //    public string LSeq;     // LoginSeq;

    //    public string HBM_AT;   // 한빛 모바일 인증 토큰
    //}
}
