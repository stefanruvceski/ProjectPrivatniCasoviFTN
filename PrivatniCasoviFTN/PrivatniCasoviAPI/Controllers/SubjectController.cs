using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace PrivatniCasoviAPI.Controllers
{
    [Authorize]
    public class SubjectController : ApiController
    {
        IContract proxy = null;
        [HttpGet]
        [Route("api/subject/getall")]
        public List<string> GetAll()
        {
            Connect();
            return proxy.GetAllSubjects();
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
