using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPILib
{
    public static class ReqTestEcho
    {
        public static RES_DEV_ECHO Process(REQ_DEV_ECHO requestPacket)
        {
            var responseResult = new RES_DEV_ECHO();
            responseResult.Result = true;
            responseResult.ResData = string.Format("WaitSec: {0}, ReqData: {1}", requestPacket.WaitSec, requestPacket.ReqData);
            return responseResult;
        }
    }
}
