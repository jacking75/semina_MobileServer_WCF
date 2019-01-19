using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace WCFTCPServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // host 생성, address 지정
            ServiceHost host = new ServiceHost(
                typeof(WCFServerLib.ServerService),
                new Uri("net.tcp://localhost/WCF/ServerService"));

            // 종점 설정
            host.AddServiceEndpoint(
                typeof(WCFServerLib.IServerService),
                new NetTcpBinding(),
                "");

            // 호스트 open
            host.Open();

            // Console 에 키 입력되면 서비스 종료
            System.Console.WriteLine("press any key to stop service");
            System.Console.ReadKey(true);
            host.Close();
        }
    }
}
