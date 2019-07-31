using Common;
using Common.BindingModel;
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

        #region proxy
        ISubjectContract proxy = null;
        #endregion

        #region api/subject/addnewsubject
        [HttpGet]
        [Route("api/subject/addnewsubject")]
        public async Task<IHttpActionResult> AddNewSubject(string subject)
        {
            List<string> retVal = new List<string>();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();

                int flag = proxy.AddNewSubject(subject);

                if (flag == 1)
                    return Ok();
                else if (flag == -1)
                    return BadRequest("Subject alredy exist.");
            }

            return BadRequest("Inner error");
        }
        #endregion

        #region api/subject/getall
        [HttpGet]
        [Route("api/subject/getall")]
        public List<string> GetAllSubjects()
        {
            Connect();
            return proxy.GetAllSubjects();
        }
        #endregion

        #region api/subject/getsubjectbyname
        [HttpGet]
        [Route("api/subject/getsubjectbyname")]
        public SubjectBindingModel GetSubjectByName(string name)
        {
            Connect();
            return proxy.GetSubjectByName(name);
        }
        #endregion

        #region api/subject/getsubjectteachers
        [HttpGet]
        [Route("api/subject/getsubjectteachers")]
        public async Task<List<string>> GetSubjectTeachers(string subject)
        {
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviSecretaries"))
            {
                Connect();
                return proxy.GetSubjectTeachers(subject);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region api/subject/getteachersubjects
        [HttpGet]
        [Route("api/subject/getteachersubjects")]
        public TeacherSubjectBindingModel GetTeacherSubjects(string id)
        {
            TeacherSubjectBindingModel retVal = new TeacherSubjectBindingModel();

            Connect();
            retVal = proxy.GetTeacherSubjects(id);


            return retVal;
        }
        #endregion

        #region api/subject/getnotteachersubjects
        [HttpGet]
        [Route("api/subject/getnotteachersubjects")]
        public async Task<List<string>> GetNotTeacherSubjectsAsync()
        {
            List<string> retVal = new List<string>();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                Connect();
                retVal = proxy.GetNotTeacherSubjects(User.Identity.Name);
            }

            return retVal;
        }
        #endregion

        #region api/subject/teacheraddnewteachingsubject
        [HttpGet]
        [Route("api/subject/teacheraddnewteachingsubject")]
        public async Task<IHttpActionResult> TeacherAddNewTeachingSubjectAsync(string subject)
        {
            List<string> retVal = new List<string>();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                Connect();

                if (proxy.TeacherAddNewTeachingSubject(User.Identity.Name, subject))
                {
                    return Ok();
                }
            }

            return BadRequest("Inner error");
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
                ChannelFactory<ISubjectContract> factory = new ChannelFactory<ISubjectContract>(netTcpBinding, new EndpointAddress($"net.tcp://localhost:11002/SubjectInputRequest"));
                proxy = factory.CreateChannel();
            }

        }
        #endregion

    }


    
}
