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
        #region proxy
        IPrivateClassContract proxy = null;
        #endregion

        #region api/privateclass/getprivateclasses
        [HttpGet]
        [Route("api/privateclass/getprivateclasses")]
        public async Task<List<PrivateClassBindingModel>> GetPrivateCLasses()
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                try
                {

                    return proxy.GetPrivateClassesForUser(User.Identity.Name, "PrivatniCasoviStudents");
                }
                catch(Exception e) {
                    return new List<PrivateClassBindingModel>();
                }
            }
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                return proxy.GetPrivateClassesForUser(User.Identity.Name, "PrivatniCasoviTeachers");
            }
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                return proxy.GetPrivateClassesForUser(User.Identity.Name, "PrivatniCasoviSecretaries");
            }
            return null;
        }
        #endregion

        #region api/privateclass/acceptClass
        [HttpGet]
        [Route("api/privateclass/acceptClass")]
        public async Task<IHttpActionResult> AcceptClass(string id)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                int flag = proxy.AcceptClass(id, User.Identity.Name);

                if (flag == -2)
                {
                    return BadRequest("You dont teach this subject.");
                }
                else if (flag == -1)
                {
                    return BadRequest("Inner error");
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("Not Authorized");
            }

        }
        #endregion

        #region api/privateclass/joinclass
        [HttpGet]
        [Route("api/privateclass/joinclass")]
        public async Task<IHttpActionResult> JoinClass(string classId)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                switch (proxy.JoinClass(classId, User.Identity.Name))
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
        #endregion

        #region api/privateclass/addClass
        [HttpPost]
        [Route("api/privateclass/addClass")]
        public async Task<IHttpActionResult> AddClass(AddPrivateClassBindingModel model)
        {
            Connect();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviStudents"))
            {
                if (proxy.StudentAddClass(model, User.Identity.Name))
                    return Ok();
                else
                    return BadRequest("Inner error");
            }
            else if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                if (proxy.SecretaryAddClass(model, User.Identity.Name))
                    return Ok();
                else
                    return BadRequest("Inner error");
            }
            else
            {
                return BadRequest("Not Authorized");
            }
        }
        #endregion

        #region api/privateclass/assignClass
        [HttpGet]
        [Route("api/privateclass/assignClass")]
        public async Task<IHttpActionResult> AssignClass(string classId, string teacher)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();
                int flag = proxy.AssignClass(classId, teacher);

                if (flag == -2)
                {
                    return BadRequest("You dont teach this subject.");
                }
                else if (flag == -1)
                {
                    return BadRequest("Inner error");
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("Not Authorized.");
            }
        }
        #endregion

        #region api/privateclass/studentdeclineclass
        [HttpGet]
        [Route("api/privateclass/studentdeclineclass")]
        public async Task<IHttpActionResult> UserDeclineClass(string classId)
        {
            if (!await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                Connect();
                if (proxy.StudentDeclineClass(User.Identity.Name, classId))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Inner error");
                }
            }
            else
            {
                return BadRequest("Not Authorized.");
            }
        }
        #endregion

        #region api/privateclass/teacherdeleteClass
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
        #endregion

        #region api/privateclass/teacherdeclineclass
        [HttpGet]
        [Route("api/privateclass/teacherdeclineclass")]
        public async Task<IHttpActionResult> TeacherDeclineClass(string classId)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                Connect();
                if (proxy.TeacherDeclineClass(User.Identity.Name, classId))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Inner error");
                }
            }
            else
            {
                return BadRequest("Not Authorized.");
            }
        }
        #endregion

        #region api/privateclass/secretarydeclineclass
        [HttpGet]
        [Route("api/privateclass/secretarydeclineclass")]
        public async Task<IHttpActionResult> SecretaryDeclineClass(string classId)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();
                if (proxy.SecretaryDeclineClass(classId))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Inner error");
                }
            }
            else
            {
                return BadRequest("Not Authorized.");
            }

        }
        #endregion

        #region api/privateclass/changedate
        [HttpGet]
        [Route("api/privateclass/changedate")]
        public IHttpActionResult ChangeDate(string classId, string date)
        {
            Connect();
            bool flag;
            string retVal = proxy.UserChangeDate(User.Identity.Name, classId, date, out flag);
            if (flag)
            {
                return Ok();
            }
            else
            {
                return BadRequest(retVal);
            }
        }
        #endregion

        #region api/privateclass/getallclassstudents
        [HttpGet]
        [Route("api/privateclass/getallclassstudents")]
        public async Task<List<string>> GetAllClassStudents(string classId)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();
                return proxy.GetAllClassStudents(classId);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region api/privateclass/removeclassstudents
        [HttpGet]
        [Route("api/privateclass/removeclassstudents")]
        public async Task<IHttpActionResult> RemoveClassStudents(string classId, string students)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();
                if (proxy.RemoveClassStudents(students, classId))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Inner error.");
                }
            }
            else
            {
                return BadRequest("Not Authorized.");
            }
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
                ChannelFactory<IPrivateClassContract> factory = new ChannelFactory<IPrivateClassContract>(netTcpBinding, new EndpointAddress($"net.tcp://localhost:11001/PrivateClassInputRequest"));
                proxy = factory.CreateChannel();
            }

        }
        #endregion
    }
}
