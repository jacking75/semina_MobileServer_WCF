using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI_SelfHosting.Request
{
    public class LoginController : ApiController
    {
        [Route("Request/Login")]
        [HttpPost]
        public LoginResponse RequestLogin(LoginRequest request)
        {
            if(request.UserPW != "123qwe")
            {
                return new LoginResponse { Result = "Error" };
            }

            return new LoginResponse { Result = "Success", Sequence = System.DateTime.Now.Ticks.ToString(), UserSeq = request.UserSeq };
        }


        public class LoginRequest
        {
            public int UserSeq { get; set; }
            public string UserID { get; set; }
            public string UserPW { get; set; }
        }

        public class LoginResponse
        {
            public string Result { get; set; }
            public string Sequence { get; set; }
            public int UserSeq { get; set; }
        }
               
    }
}
