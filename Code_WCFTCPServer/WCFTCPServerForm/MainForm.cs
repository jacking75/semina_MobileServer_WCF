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

using ServerLogicLib;

namespace WCFTCPServerForm
{
    public partial class MainForm : Form
    {
        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();

        ServiceHost Host = null;
        WCFServerLib.ServerService HostService = null;
        ServerLogic MainLogic = new ServerLogic();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();

            InitWCF();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Host.Close();
        }
                
        private void 클라이언트짜르기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var sel in listBoxClient.SelectedItems)
            {
                MainLogic.ForceDisconnect(sel.ToString());
            }
        }

        private void 클라이언트에메시지보내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxMessage.Text))
            {
                MessageBox.Show("클라이언트에 보낼 메시지가 없어요");
                return;
            }

            foreach (var sel in listBoxClient.SelectedItems)
            {
                MainLogic.SendPacketClient(sel.ToString(), textBoxMessage.Text);
            }
        }




        void InitWCF()
        {
            MainLogic = new ServerLogic();
            MainLogic.Init();

            HostService = new WCFServerLib.ServerService(MainLogic);

            // host 생성, address 지정
            Host = new ServiceHost(HostService, new Uri("net.tcp://localhost/WCF/ServerService"));
           
            // 종점 설정
            Host.AddServiceEndpoint(
                typeof(WCFServerLib.IServerService),
                new NetTcpBinding(),
                "");

            
            // 호스트 open
            Host.Open();
        }

        private void OnProcessTimedEvent(object sender, EventArgs e)
        {
            try
            {
                ProcessLog();
                ProcessInnerMessage();
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

        void ProcessInnerMessage()
        {
            InnerMsg msg;

            if (InnerMessageHostProgram.GetMsg(out msg) == false)
            {
                return;
            }

            switch (msg.Type)
            {
                case InnerMsgType.SERVER_START:
                    break;

                case InnerMsgType.CREATE_COMPONENT:
                    break;

                case InnerMsgType.REGIST_NEW_USER:
                    listBoxClient.Items.Add(msg.Value1);
                    break;

                case InnerMsgType.UNREGIST_USER:
                    listBoxClient.Items.Remove(msg.Value1);
                    break;

                case InnerMsgType.CURRENT_CONNECT_COUNT:
                    break;
                                                    
                case InnerMsgType.UPDATE_UI_INFO:
                    break;
            }
        }

        

        




        
    }
}
