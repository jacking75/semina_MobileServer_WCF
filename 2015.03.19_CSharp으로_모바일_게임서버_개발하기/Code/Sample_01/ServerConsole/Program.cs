using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Description;


namespace ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 멀티코어 JIT 유효화
                System.Runtime.ProfileOptimization.SetProfileRoot(Environment.CurrentDirectory);
                System.Runtime.ProfileOptimization.StartProfile("App.JIT.Profile");

                ServiceHost host = new ServiceHost(typeof(RESTfulAPILib.APIService));

                foreach (ServiceEndpoint ep in host.Description.Endpoints)
                {
                    Console.WriteLine("바인딩 타입: {0}, Address:{1}", ep.Binding.ToString(), ep.Address.ToString());
                                        
                    var binding = ep.Binding as WebHttpBinding;
                   Console.WriteLine("최대버퍼:{0}, 최대버퍼풀:{1}, 최대받기크기:{2}",
                                binding.MaxBufferSize, binding.MaxBufferPoolSize, binding.MaxReceivedMessageSize);
                }

                host.Open();

                Console.WriteLine("WCF 호스트 생성 완료");
                Console.WriteLine("Running Server GC Mode: " + System.Runtime.GCSettings.IsServerGC);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            int workerThreads, completionPortThreads = 0;
            System.Threading.ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("- 최대 워커 스레드 수: {0}, 최대 IO 스레드 수:{1} --", workerThreads, completionPortThreads);

            System.Threading.ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("- 최소 워커 스레드 수: {0}, 최소 IO 스레드 수:{1} --", workerThreads, completionPortThreads);

            System.Threading.ThreadPool.SetMinThreads(128, 128);
            System.Threading.ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine("- 최소 워커 스레드 수: {0}, 최소 IO 스레드 수:{1} --", workerThreads, completionPortThreads);

            Console.WriteLine("서비스 시작 시간: " + DateTime.Now.ToString());

            //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(키보드입력조사), null);

            while (true)
            {
                System.Threading.Thread.Sleep(128);
            }
        }

        static void 키보드입력조사(object userState)
        {
            while (true)
            {
                try
                {
                    var command = Console.ReadLine();

                    if (command == "종료준비")
                    {
                        //ServerLogic.ServerInit.EnableRequestHeathCheck = false;
                        Console.WriteLine("서버 종료 준비 요청. 앞으로 AWS의 로드밸런스 상태체크를 에러로 답변합니다");
                    }
                    else if (command == "정상")
                    {
                        //ServerLogic.ServerInit.EnableRequestHeathCheck = true;
                        Console.WriteLine("서버 정상 요청. 앞으로 AWS의 로드밸런스 상태체크를 정상으로 답변합니다");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


    }
}
