using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerLib
{
    [ServiceContract]
    public interface IServerService
    {
        [OperationContract]
        Person GetData(string id);

        [OperationContract]
        LoginResult Login(string id, string pw);
       
    }

   
}
