using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.WebSockets;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace WCFWebSocketConsole
{
    // 참고: http://stackoverflow.com/questions/24239953/wcf-self-hosted-websocket-service-with-javascript-client
    // async/await 사용 버전 http://www.tagwith.com/question_1916644_wcf-websocket-unable-to-connect-from-browser
    class Program
    {
        static void Main(string[] args)
        {

            Uri baseAddress = new Uri("http://localhost:8080/hello");

            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(WebSocketsServer), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                CustomBinding binding = new CustomBinding();
                binding.Elements.Add(new ByteStreamMessageEncodingBindingElement());
                HttpTransportBindingElement transport = new HttpTransportBindingElement();
                //transport.WebSocketSettings = new WebSocketTransportSettings();
                transport.WebSocketSettings.TransportUsage = WebSocketTransportUsage.Always;
                transport.WebSocketSettings.CreateNotificationOnConnection = true;
                
                // tcp 레이어 단에서 동작하는 것 같음. 클라이언트에서 패킷을 안보내고 있어도 연결 끊어지지 않음
                //transport.WebSocketSettings.KeepAliveInterval = new TimeSpan(0, 1, 0);
                
                binding.Elements.Add(transport);

                host.AddServiceEndpoint(typeof(IWebSocketsServer), binding, "");

                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }

    [ServiceContract(CallbackContract = typeof(IProgressContext))]
    public interface IWebSocketsServer
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void SendMessageToServer(Message msg);
    }

    [ServiceContract]
    interface IProgressContext
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ReportProgress(Message msg);
    }

    public class WebSocketsServer : IWebSocketsServer
    {
        public void SendMessageToServer(Message msg)
        {
            var ctx = OperationContext.Current;
            var client = ctx.GetCallbackChannel<IProgressContext>();

            if (msg.IsEmpty) // 접속 이후에는 데이터 없이 send를 보내도 IsEmpty는 true가 아니다. 
            {
                //여기에 들어오면 처음 접속한 것이다.
                OperationContext.Current.Channel.Closed += new EventHandler(Channel_Closed);
                return;
            }
            ///var callback = OperationContext.Current.GetCallbackChannel<IProgressContext>();
            //if (msg.IsEmpty || ((IChannel)client).State != CommunicationState.Opened)
            //{
            //    // 여기에 들어오면 처음 접속한 것이다.
            //    //OperationContext.Current.Channel.Closed += new EventHandler(Channel_Closed);
            //    return;
            //}
            

            byte[] body = msg.GetBody<byte[]>();
            string msgTextFromClient = Encoding.UTF8.GetString(body);

            string msgTextToClient = string.Format(
                "Got message {0} at {1}",
                msgTextFromClient,
                DateTime.Now.ToLongTimeString());

            client.ReportProgress(CreateMessage(msgTextToClient));
        }

        private Message CreateMessage(string msgText)
        {
            Message msg = ByteStreamMessage.CreateMessage(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(msgText)));

            msg.Properties["WebSocketMessageProperty"] =
                new WebSocketMessageProperty
                {
                    MessageType = WebSocketMessageType.Text
                };

            return msg;
        }
        
        void Channel_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Closed Client");
        }
    }
}
