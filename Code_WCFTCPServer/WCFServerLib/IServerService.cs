using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace WCFServerLib
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void RegistClinet(string id);



        [OperationContract]
        void TestUserData(RequestEcho reqEcho);

        [OperationContract]
        string TestSayHello();
    }
}
