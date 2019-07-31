using Common;
using Common.BindingModels;
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
    public class UserController : ApiController
    {
        #region proxy
        IUserContract proxy = null;
        #endregion

        #region api/users/edit
        [HttpPost]
        [Route("api/users/edit")]
        public async System.Threading.Tasks.Task<IHttpActionResult> EditAsync(EditUserInfoBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.Email = User.Identity.Name;
            Connect();
            string type = "";
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
                type = "PrivatniCasoviStudents";
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
                type = "PrivatniCasoviTeachers";
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
                type = "PrivatniCasoviSecretaries";
            try
            {

                if (!proxy.EditUserInformations(model, type))
                    return BadRequest("Internal error");
            }
            catch
            {

            }
            return Ok();
        }
        #endregion

        #region api/users/onsignin
        [HttpGet]
        [Route("api/users/onsignin")]
        public async System.Threading.Tasks.Task<EditUserInfoBindingModel> OnSingIn()
        {
            Connect();

            string type = "";
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
                type = "PrivatniCasoviStudents";
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
                type = "PrivatniCasoviTeachers";
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
                type = "PrivatniCasoviSecretaries";

            EditUserInfoBindingModel retVal = proxy.GetUserByEmail(User.Identity.Name, type);
            retVal.Username += "_" + await AuthorizationHelper.GetGroupName();

            return retVal;
        }
        #endregion

        #region api/users/getuserinfo
        [HttpGet]
        [Route("api/users/getuserinfo")]
        public EditUserInfoBindingModel GetUserInfo()
        {
            Connect();
            return proxy.GetUserForEdit(User.Identity.Name);
        }
        #endregion

        #region api/users/getallteachers
        [HttpGet]
        [Route("api/users/getallteachers")]
        public List<EditUserInfoBindingModel> GetAllMathTeachers(string type)
        {
            Connect();
            return proxy.GetAllMathTeachers(type);
        }
        #endregion

        #region WCF Connection
        private void Connect()
        {
            if (proxy == null)
            {
                NetTcpBinding netTcpBinding = new NetTcpBinding();
                netTcpBinding.MaxBufferSize = int.MaxValue;
                netTcpBinding.MaxReceivedMessageSize = int.MaxValue;
                netTcpBinding.MaxBufferPoolSize = int.MaxValue;
                ChannelFactory<IUserContract> factory = new ChannelFactory<IUserContract>(netTcpBinding, new EndpointAddress($"net.tcp://localhost:11000/UserInputRequest"));
                proxy = factory.CreateChannel();
            }

        }
        #endregion
    }
}
