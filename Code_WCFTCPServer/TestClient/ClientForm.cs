using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace TestClient
{
    public partial class ClientForm : Form
    {
        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();

        WCFServerLib.IServerService ServerProxy = null;


        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();

            InitWCF();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerProxy != null)
            {
                var channel = (System.ServiceModel.Channels.IChannel)ServerProxy;
                if(channel.State == CommunicationState.Opened)
                {
                    channel.Close();
                }
            }
        }

        // 서버에 등록하기
        private void button2_Click(object sender, EventArgs e)
        {
            ServerProxy.RegistClinet(textBoxUserID.Text);
        }

        // 서버에 메시지 보내기
        private void button1_Click(object sender, EventArgs e)
        {
            if (ServerProxy == null)
            {
                return;
            }

            // 서비스의 메소드 호출
            var result = ServerProxy.TestSayHello();
            DevLog.Write(result);
        }

        // 에코 메시지 보내기 
        private void button3_Click(object sender, EventArgs e)
        {
            if (ServerProxy == null)
            {
                return;
            }

            var request = new WCFServerLib.RequestEcho 
            { 
                Message = string.Format("[{0}] 클라이언트에서 보냈음^^", DateTime.Now) 
            };
            
            ServerProxy.TestUserData(request);
        }



        void InitWCF()
        {
            try
            {
                var callback = new ClientCallBackHandler();

                var factory = new DuplexChannelFactory<WCFServerLib.IServerService>(callback);
                factory.Endpoint.Binding = new NetTcpBinding();
                factory.Endpoint.Contract.ContractType = typeof(WCFServerLib.IServerService);
                factory.Endpoint.Address = new EndpointAddress(textBoxServer.Text);

                ServerProxy = factory.CreateChannel();                                
            }
            catch (Exception ex)
            {
                DevLog.Write(ex.Message);
            }
        }

        

        private void OnProcessTimedEvent(object sender, EventArgs e)
        {
            try
            {
                ProcessLog();
            }
            catch (Exception ex)
            {
                DevLog.Write(string.Format("[OnProcessTimedEvent] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
            }
        }

        private void ProcessLog()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

                    if (listBoxLog.Items.Count > 512)
                    {
                        listBoxLog.Items.Clear();
                    }

                    listBoxLog.Items.Add(msg);
                    listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                }
                else
                {
                    break;
                }

                if (logWorkCount > 32)
                {
                    break;
                }
            }
        }

        

        

        
        

        
    }
}
