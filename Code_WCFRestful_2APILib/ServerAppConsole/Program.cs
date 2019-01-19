using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ServerAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            //Uri httpUrl = new Uri("http://localhost:8732/ServerService");
            
            // Create ServiceHost 1
            ServiceHost host1 = new ServiceHost(typeof(ServerLib.ServerService));
            
            //Add a service endpoint
            //host.AddServiceEndpoint(typeof(ServerLib.IServerService), new WebHttpBinding(), "");
            
            //Enable metadata exchange
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(smb);

            foreach (ServiceEndpoint ep in host1.Description.Endpoints)
            {
                Console.WriteLine(ep.ToString());
            }

            //Start the Service
            host1.Open();


            // Create ServiceHost 2
            ServiceHost host2 = new ServiceHost(typeof(ServerLib2.ServerService2));

            foreach (ServiceEndpoint ep in host2.Description.Endpoints)
            {
                Console.WriteLine(ep.ToString());
            }

            //Start the Service
            host2.Open();
            
            

            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}
