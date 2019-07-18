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
        [HttpGet]
        [Route("api/subject/getnotteachersubjects")]
        public async Task<List<string>> GetNotTeacherSubjectsAsync()
        {
            List<string> retVal = new List<string>();
            if (await AuthorizationHelper.IsInGroup("PrivatniCasoviTeachers"))
            {
                Connect();
               retVal =  proxy.GetNotTeacherSubjectsAsync(User.Identity.Name);
            }

            return retVal;
        }
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



        private void Connect()
        {
            if (proxy == null)
            {
                NetTcpBinding netTcpBinding = new NetTcpBinding();
                netTcpBinding.MaxBufferSize = int.MaxValue;
                netTcpBinding.MaxReceivedMessageSize = int.MaxValue;
                netTcpBinding.MaxBufferPoolSize = int.MaxValue;
                ChannelFactory<IContract> factory = new ChannelFactory<IContract>(netTcpBinding, new EndpointAddress($"net.tcp://localhost:11000/InputRequest"));
                proxy = factory.CreateChannel();
            }

        }
    }


    
}
