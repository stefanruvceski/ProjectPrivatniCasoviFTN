using Common;
using Common.Models;
using Common.Utils;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace PrivatniCasoviAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        IContract proxy = null;
        // GET api/values
        [Route("api/values/get")]
        public async Task<IEnumerable<string>> Get(string id)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviUsers"))
            {
                Connect();
                string p = proxy.test(id);
                return new string[] { p };
            }
            else
            {
                return new string[] { "Ne mere" };
            }

        }

        private void Connect()
        {
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(new NetTcpBinding(), new EndpointAddress($"net.tcp://localhost:11000/InputRequest"));
            proxy = factory.CreateChannel();

        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
