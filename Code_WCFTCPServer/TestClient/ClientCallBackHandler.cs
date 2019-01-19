using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class ClientCallBackHandler : WCFServerLib.IClientCallback
    {        
        public void SendMessageToClient(string message)
        {
            DevLog.Write(message);
        }

    }
}
