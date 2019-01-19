using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;




namespace ServerLib2
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServerService2 : IServerService2
    {
        // http://localhost:8732/ServerService2/data/111
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


        // http://localhost:8732/ServerService2/login/jacking.1111
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "login/{id}.{pw}")]
        public LoginResult Login(string id, string pw)
        {
            return new LoginResult()
            {
                Result = 0,
                AuthToken = "ttttsssddd22"
            };
        }

        // http://localhost:8732/ServerService2/GetItem
        [WebInvoke(Method = "POST",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetItem")]
        public ItemInfo GetItem(string itemid) 
        {
            return new ItemInfo()
            {
                version = 1,
                ID = Convert.ToInt32(itemid),
                name = "test Item",
                Lv = 1
            };
        }

        // http://localhost:8732/ServerService2/GetItem2
        [WebInvoke(Method = "POST",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetItem2")]
        public ItemInfo GetItem2(int itemid)
        {
            return new ItemInfo()
            {
                version = 1,
                ID = itemid,
                name = "test Item",
                Lv = 1
            };
        }

        // http://localhost:8732/ServerService2/GetItem3
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.WrappedRequest, 
                    UriTemplate = "GetItem3")]
        public ItemInfo GetItem3(int itemid, string itemname)
        {
            return new ItemInfo()
            {
                version = 1,
                ID = itemid,
                name = itemname,
                Lv = 1
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


    public class ItemInfo
    {
        public int version;
        public int ID;
        public string name;
        public int Lv;
    }
}
