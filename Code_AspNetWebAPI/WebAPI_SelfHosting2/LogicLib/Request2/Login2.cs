using System.Web.Http;

namespace LogicLib.Request2
{
    public class Login2Controller : ApiController
    {
        [Route("Request/Login2")]
        [HttpPost]
        public Login2Response RequestLogin(Login2Request request)
        {
            if (request.UserPW != "123qwe")
            {
                return new Login2Response { Error = "Error PW" };
            }

            return new Login2Response { Error = "none", Sequence = System.DateTime.Now.Ticks.ToString(), UserSeq = request.UserSeq };
        }


        public class Login2Request
        {
            public int UserSeq { get; set; }
            public string UserID { get; set; }
            public string UserPW { get; set; }
        }

        public class Login2Response
        {
            public string Error { get; set; }
            public string Sequence { get; set; }
            public int UserSeq { get; set; }
        }

    }
}
