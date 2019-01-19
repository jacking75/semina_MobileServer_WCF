using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogicLib
{
    public class InnerMessageHostProgram
    {
        static System.Collections.Concurrent.ConcurrentQueue<InnerMsg> msgQueue = new System.Collections.Concurrent.ConcurrentQueue<InnerMsg>();

        static bool[] EnableInnerMsgTypeList = new bool[(int)InnerMsgType.END];


        public static void Init()
        {
            for (int i = 0; i < (int)InnerMsgType.END; ++i)
            {
                EnableInnerMsgTypeList.SetValue(true, i);
            }
        }

        public static void EnableDisable(InnerMsgType type, bool enable)
        {
            EnableInnerMsgTypeList[(int)type] = enable;
        }

        public static bool IsEnable(InnerMsgType type)
        {
            return EnableInnerMsgTypeList[(int)type];
        }


        public static bool GetMsg(out InnerMsg msg)
        {
            return msgQueue.TryDequeue(out msg);
        }

        public static void ServerStart(int ServerID, int Port)
        {
            var msg = new InnerMsg() { Type = InnerMsgType.SERVER_START };
            msg.Value1 = string.Format("{0}_{1}", ServerID, Port);

            msgQueue.Enqueue(msg);
        }

        public static void RegistNewUser(string id) 
        {
            if (IsEnable(InnerMsgType.REGIST_NEW_USER) == false)
            {
                return;
            }

            var msg = new InnerMsg() { Type = InnerMsgType.REGIST_NEW_USER };
            msg.Value1 = id;
            msgQueue.Enqueue(msg);
        }

        public static void UnRegistUser(string id) 
        {
            if (IsEnable(InnerMsgType.UNREGIST_USER) == false)
            {
                return;
            }

            var msg = new InnerMsg() { Type = InnerMsgType.UNREGIST_USER };
            msg.Value1 = id;
            msgQueue.Enqueue(msg);
        }

        public static void CurrentUserCount(int count)
        {
            if (IsEnable(InnerMsgType.CURRENT_CONNECT_COUNT) == false)
            {
                return;
            }

            var msg = new InnerMsg() { Type = InnerMsgType.CURRENT_CONNECT_COUNT };
            msg.Value1 = count.ToString();
            msgQueue.Enqueue(msg);
        }
                
        public static void UIUpdateInfo(string userCount, string lobbyUserCount)
        {
            var msg = new InnerMsg() { Type = InnerMsgType.UPDATE_UI_INFO };
            msg.Value1 = string.Format("{0}_{1}", userCount, lobbyUserCount);
            msgQueue.Enqueue(msg);
        }
    }

    public enum InnerMsgType
    {
        SERVER_START = 0,
        CREATE_COMPONENT,
        REGIST_NEW_USER,
        UNREGIST_USER,
        CURRENT_CONNECT_COUNT,
        CURRENT_LOBBY_INFO,
        UPDATE_UI_INFO,

        END
    }

    public class InnerMsg
    {
        public InnerMsgType Type;
        public string SessionID;
        public string Value1;
        public string Value2;
    }
}
