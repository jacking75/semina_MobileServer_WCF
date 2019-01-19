using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 에러코드 선언
namespace APILogicLib
{
    public enum ERROR_ID
    {
        NONE        = 0,

        REDIS_START_PARSE_DB_CONNECT_STRING = 101,
        REDIS_START_EXCEPTION = 102,
        REDIS_START_SET_TEST = 103,

        LOAD_CONFIG_MONGODB = 121,
        LOAD_CONFIG_REDIS = 122,
        
        EXCEPTION_GAME_CONTENT_LOAD = 131,


        FAIL_NETWORK_HTTP_REQUEST = 151,
        EXCEPTION_HTTP_REQUEST = 152,

        
        REQUEST_PACKET_DECRYPT = 501,

        PREV_REQUEST_NOT_COMPLETE = 521,
        PREV_REQUEST_FAIL_REDIS = 522,


        CREATE_ACCOUNT_DUPLICATE = 601,
        CREATE_ACCOUNT_DB_FAIL = 602,


        LOG_IN_PW = 621,


        LOAD_USER_GAME_DATA_AUTH = 641,



        // <<<<< DEV  >>>>
        CLIENT_HTTP_NETWOR_EXCEPTION = 20001,
        CLIENT_HTTP_NETWOR_CONNECT = 20002,
    }
}
