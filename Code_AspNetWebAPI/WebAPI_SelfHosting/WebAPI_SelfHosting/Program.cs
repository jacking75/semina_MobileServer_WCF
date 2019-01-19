using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin.Hosting;
using System.Net.Http;

namespace WebAPI_SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            //string baseAddress = "http://localhost:19000/"; // 내부에서만 접속하는 경우는 localhost 사용
            //string baseAddress = "http://10.73.44.51:19000/"; // 외부에서 접속할 때 서버 실행 머신의 IP를 사용
            string baseAddress = "http://*:19000/"; // 외부에서 접속할 때 할당된 IP를 자동으로 사용

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Web API 실행 중: " + baseAddress);
                Console.ReadLine();
            }            
        }
    }
}
