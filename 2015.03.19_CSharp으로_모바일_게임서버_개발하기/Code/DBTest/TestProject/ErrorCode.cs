using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public enum ERROR_ID
    {
        NONE        = 0,

        REDIS_START_PARSE_DB_CONNECT_STRING = 101,
        REDIS_START_EXCEPTION = 102,
        REDIS_START_SET_TEST = 103,

        
    }
}
