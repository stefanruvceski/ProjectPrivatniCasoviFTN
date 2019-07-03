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
    public class PrivateClassController : ApiController
    {
        IContract proxy = null;

        [HttpGet]
        [Route("api/privateclass/getprivateclasses")]
        public async Task<List<PrivateClassBindingModel>> GetPrivateCLassesAsync()
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                return proxy.GetPrivateClassesForUser(User.Identity.Name, "PrivatniCasoviStudents");
            }
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                return proxy.GetPrivateClassesForUser(User.Identity.Name, "PrivatniCasoviTeachers");
            }
            return null;
        }

        [HttpGet]
        [Route("api/privateclass/acceptClass")]
        public async Task<IHttpActionResult> AcceptClass(string id)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                proxy.AcceptClass(id, User.Identity.Name);
                return Ok();
            }
            else
            {
                return BadRequest("Not Authorized");
            }
            
        }

        [HttpGet]
        [Route("api/privateclass/teacherdeleteClass")]
        public async Task<IHttpActionResult> DeleteClass(string id)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers") || await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                proxy.TeacherDeleteClass(id);
                return Ok();
            }
            else
            {
                return BadRequest("Not Authorized");
            }

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
