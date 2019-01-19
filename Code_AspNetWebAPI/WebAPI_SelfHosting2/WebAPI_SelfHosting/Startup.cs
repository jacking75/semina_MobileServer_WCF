using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Owin;
using System.Web.Http;

namespace WebAPI_SelfHosting
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            LogicLib.MyApiConfig.Register(config);

            appBuilder.UseWebApi(config);
        }
    }
}
