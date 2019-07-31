using Common.BindingModel;
using Common.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ISubjectContract
    {
        [OperationContract]
        List<string> GetAllSubjects();
      
        [OperationContract]
        List<string> GetSubjectTeachers(string subject);

        [OperationContract]
        List<string> GetNotTeacherSubjects(string email);

        [OperationContract]
        bool TeacherAddNewTeachingSubject(string email, string subject);
        [OperationContract]
        int AddNewSubject(string subject);

        [OperationContract]
        SubjectBindingModel GetSubjectByName(string name);

        [OperationContract]
        TeacherSubjectBindingModel GetTeacherSubjects(string teacherId);
    }
}
