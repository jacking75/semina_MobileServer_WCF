using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using System.ServiceModel;

namespace ServerLogicLib
{
    public class ServerLogic
    {
        ConcurrentDictionary<string, Client> ClientMap = new ConcurrentDictionary<string, Client>();
        ConcurrentDictionary<string, string> SessionIDMap = new ConcurrentDictionary<string, string>();

        public Func<string, bool> ForceDisconnect;
        public Func<string, string, bool> SendPacketClient;
        public Func<string, int> SendPacketAllClient;


        public void Init()
        {
            InnerMessageHostProgram.Init();
        }

        public void AddClinet(string id, IClientChannel clientChannel)
        {
            var client = new Client();
            ClientMap.TryAdd(id, client);
            SessionIDMap.TryAdd(clientChannel.SessionId, id);

            InnerMessageHostProgram.RegistNewUser(id);
            DevLog.Write(string.Format("[AddClinet] ID:{0}, SessionID:{1}", id, clientChannel.SessionId), LOG_LEVEL.INFO);
        }

        public string RemoveClient(string sessionID)
        {
            string id = null;
            if (SessionIDMap.TryRemove(sessionID, out id) == false)
            {
                return id;
            }

            Client tempClient = null;
            ClientMap.TryRemove(id, out tempClient);

            InnerMessageHostProgram.UnRegistUser(id);
            DevLog.Write(string.Format("[RemoveClinet] ID:{0}, SessionID:{1}", id, sessionID), LOG_LEVEL.INFO);

            return id;
        }
    }
}
