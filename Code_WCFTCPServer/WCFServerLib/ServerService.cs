using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using System.ServiceModel;

using ServerLogicLib;

namespace WCFServerLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServerService : IServerService
    {
        ServerLogic MainLogic;

        ConcurrentDictionary<string, IClientCallback> ClientMap = new ConcurrentDictionary<string, IClientCallback>();

        

        public ServerService(ServerLogic mainLogic)
        {
            MainLogic = mainLogic;
            
            MainLogic.ForceDisconnect = this.ForceDisconnect;
            MainLogic.SendPacketClient = this.SendToClient;
            MainLogic.SendPacketAllClient = this.BrodCast;
        }

        /// <summary>
        /// 클라이언트 호출 API들 시작
        /// </summary>
        public void RegistClinet(string id)
        {
            var ctx = OperationContext.Current;
            var client = ctx.GetCallbackChannel<IClientCallback>();
            IClientChannel channel = client as IClientChannel;

            if (ClientMap.TryAdd(id, client))
            {
                MainLogic.AddClinet(id, channel);

                // 네트워크 통신이 안될 때 호출
                //OperationContext.Current.Channel.Faulted += new EventHandler(Channel_Faulted);
                // 클라이언트의 접속이 우아하게 종료되었을 때 호출
                OperationContext.Current.Channel.Closed += new EventHandler(Channel_Closed);

                client.SendMessageToClient(string.Format("Success. [ID:{0}]: 서버에 등록 되었습니다", id));
            }
            else
            {
                client.SendMessageToClient(string.Format("Fail. [ID:{0}]: 이미 서버에 등록 되어 있습니다.", id));
            }
        }


        public void TestUserData(RequestEcho reqEcho)
        {
            DevLog.Write(string.Format("[RequestEcho] 메시지:{0}", reqEcho.Message), LOG_LEVEL.INFO);
        }

        public string TestSayHello()
        {
            return "Hello WCF World!";
        }
        /// <summary>
        /// 클라이언트 호출 API들 끝
        /// </summary>
        
         
        public bool SendToClient(string id, string message)
        {
            IClientCallback client = null;
            if (ClientMap.TryGetValue(id, out client) == false)
            {
                return false;
            }

            client.SendMessageToClient(message);

            return true;
        }


        void Channel_Closed(object sender, EventArgs e)
        {
            Logout((IContextChannel)sender);
        }

        void Logout(IContextChannel channel)
        {
            string sessionId = null;

            if (channel != null)
            {
                sessionId = channel.SessionId;
                var userID = MainLogic.RemoveClient(sessionId);

                if (userID != null)
                {
                    IClientCallback client = null;
                    ClientMap.TryRemove(userID, out client);
                }
            }
        }

        bool ForceDisconnect(string id)
        {
            IClientCallback client = null;
            if (ClientMap.TryGetValue(id, out client) == false)
            {
                return false;
            }

            IClientChannel channel = client as IClientChannel;
            ((ICommunicationObject)channel).Close();
            return true;
        }

        int BrodCast(string message)
        {
            int count = 0;

            foreach (var client in ClientMap.Values)
            {
                var channel = client as IClientChannel;
                if (channel.State != CommunicationState.Opened)
                {
                    continue;
                }
                
                client.SendMessageToClient(message);
                ++count;
            }

            return count;
        }


                
    }
}
