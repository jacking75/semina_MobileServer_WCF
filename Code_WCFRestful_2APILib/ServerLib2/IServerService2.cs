using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerLib2
{
    [ServiceContract]
    public interface IServerService2
    {
        [OperationContract]
        Person GetData(string id);

        [OperationContract]
        LoginResult Login(string id, string pw);

        [OperationContract]
        ItemInfo GetItem(string itemid);

        [OperationContract]
        ItemInfo GetItem2(int itemid);

        [OperationContract]
        ItemInfo GetItem3(int itemid, string itemname);
    }

   
}
