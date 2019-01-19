using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib
{
    public enum ERROR_CODE
    {
        NONE        = 0,


        REQ_CREATE_USER_INVALID_ID = 101,
        REQ_CREATE_USER_DUPLICATE_USER_ID = 102,

        REQ_LOGIN_INVALID_USER = 111,
        REQ_LOGIN_PW = 112,

        REQ_LOAD_BASIC_GAME_DATA_INVALID_AUTH = 121,
        REQ_LOAD_BASIC_GAME_DATA_INVALID_ID = 122,
    }
}
