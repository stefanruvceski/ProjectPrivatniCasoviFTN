using Common;
using Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace PrivatniCasoviAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        IContract proxy = null;
        [HttpPost]
        [Route("api/users/edit")]
        public IHttpActionResult Edit(EditUserInfoBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.Email = User.Identity.Name;
            Connect();

            if (!proxy.EditUserInformations(model))
                return BadRequest("Internal error");

            return Ok();
        }

        [HttpGet]
        [Route("api/users/onsignin")]
        public EditUserInfoBindingModel OnSingIn()
        {
            Connect();
            return proxy.GetUserByEmail(User.Identity.Name);
        }

        [HttpGet]
        [Route("api/users/getuserinfo")]
        public EditUserInfoBindingModel GetUserInfo()
        {
            Connect();
            return proxy.GetUserForEdit(User.Identity.Name);
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
