using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel.Web;
using System.ServiceModel;


namespace RESTfulAPILib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class APIService : IAPIService
    {
        APIService()
        {
        }


        //[[ 테스트 관련 ]]
        // http://localhost:23333/APIService/TestEcho
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "TestEcho")]
        public RES_DEV_ECHO TestEcho(REQ_DEV_ECHO requestPacket)
        {
            var result = ReqTestEcho.Process(requestPacket);
            return result;
        }
        
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
}
