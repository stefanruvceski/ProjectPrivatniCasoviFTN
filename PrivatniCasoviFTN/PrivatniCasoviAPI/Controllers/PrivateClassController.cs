using Common;
using Common.BindingModels;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace PrivatniCasoviAPI.Controllers
{
    public class PrivateClassController : ApiController
    {
        IContract proxy = null;

        [HttpGet]
        [Route("api/privateclass/getprivateclasses")]
        public async System.Threading.Tasks.Task<List<PrivateClassBindingModel>> GetPrivateCLassesAsync()
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviUsers"))
            {
                return proxy.GetPrivateClassesForUser(User.Identity.Name,"PrivatniCasoviStudents");
            }
            return null;
        }

        private void Connect()
        {
            if (proxy == null)
            {
                ChannelFactory<IContract> factory = new ChannelFactory<IContract>(new NetTcpBinding(), new EndpointAddress($"net.tcp://localhost:11000/InputRequest"));
                proxy = factory.CreateChannel();
            }

        }
    }
}
