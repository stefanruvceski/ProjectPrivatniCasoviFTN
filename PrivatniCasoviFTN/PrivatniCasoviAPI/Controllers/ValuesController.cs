using Common;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace PrivatniCasoviAPI.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        IContract proxy = null;
        // GET api/values
        public IEnumerable<string> Get()
        {
            IConfidentialClientApplication clientApplication = AuthorizationCodeProvider.CreateClientApplication("b88120ce-65d5-4aad-8b14-a22f39b93e94", "http://localhost:52988");
            AuthorizationCodeProvider authProvider = new AuthorizationCodeProvider(clientApplication, scopes);

            User.IsInRole("User");
            User.IsInRole("Guest");
            User.IsInRole("Admin");
            Connect();
            proxy.test();
            return new string[] { "value1", "value2" };

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
