using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;




namespace ServerLib
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServerService : IServerService
    {
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "data/{id}")]
        public Person GetData(string id)
        {
            return new Person()
            {
                Id = Convert.ToInt32(id),
                Name = "Leo Messi"
            };
        }


        // http://localhost:8732/ServerService/login/jacking.1111
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "login/{id}.{pw}")]
        public LoginResult Login(string id, string pw)
        {
            int ErrorCode = 0;

            if (DBLib.MongoDBLib.Auth(id, pw) == false)
            {
                ErrorCode = 1;
            }

            return new LoginResult()
            {
                Result = ErrorCode,
                AuthToken = "ttttsssddd11"
            };
        }
                
    }


    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LoginResult
    {
        public int Result { get; set; }
        public string AuthToken { get; set; }
    }
}
