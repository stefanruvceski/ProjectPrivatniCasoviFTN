using Common;
using Common.BindingModel;
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
        [Route("api/privateclass/joinclass")]
        public async Task<IHttpActionResult> JoinClass(string classId)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                switch(proxy.JoinClass(classId, User.Identity.Name))
                {
                    case 1: 
                        return Ok();
                    case 0:
                        return BadRequest("Inner error");
                    case -1:
                        return BadRequest("Class is full");
                    case -2:
                        return BadRequest("You alredy are in class");
                    default:
                        return BadRequest();
                }
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

        [HttpPost]
        [Route("api/privateclass/add")]

        public  async Task<IHttpActionResult> AddClass(AddPrivateClassBindingModel model)
        {
            Connect();
            if (!await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                if(proxy.AddClass(model, User.Identity.Name))
                    return Ok();
                else
                    return BadRequest("Inner error");
            }
            else
            {
                return BadRequest("Not Authorized");
            }
        }

        [HttpGet]
        [Route("api/privateclass/userdeclineclass")]
        
        public IHttpActionResult UserDeclineClass(string classId)
        {
            Connect();
            if(proxy.UserDeclineClass(User.Identity.Name, classId))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Inner error");
            }
        }
        [HttpGet]
        [Route("api/privateclass/changedate")]
        public IHttpActionResult ChangeDate(string classId,string date)
        {
            Connect();
            bool flag;
            string retVal = proxy.UserChangeDate(User.Identity.Name, classId, date, out flag);
            if(flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest(retVal);
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
