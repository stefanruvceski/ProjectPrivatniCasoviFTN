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
    public interface IContract
    {
       
        [OperationContract]
        bool EditUserInformations(EditUserInfoBindingModel bindingModel, string type);
        [OperationContract]
        EditUserInfoBindingModel GetUserByEmail(string email, string type);
        [OperationContract]
        EditUserInfoBindingModel GetUserForEdit(string email);
        [OperationContract]
        List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group);
        [OperationContract]
        int AcceptClass(string classId, string email);
        [OperationContract]
        bool TeacherDeleteClass(string classId);
        [OperationContract]
        int JoinClass(string classId,string email);

        [OperationContract]
        List<string> GetAllSubjects();
        [OperationContract]
        bool StudentAddClass(AddPrivateClassBindingModel model, string email);

        [OperationContract]
        bool SecretaryAddClass(AddPrivateClassBindingModel model, string email);
        [OperationContract]
        bool StudentDeclineClass(string email, string classId);
        [OperationContract]
        bool TeacherDeclineClass(string email, string classId);
        [OperationContract]
        string UserChangeDate(string email, string classId, string date,out bool flag);
        [OperationContract]
        List<string> GetSubjectTeachers(string subject);
        [OperationContract]
        int AssignClass(string classId, string teacher);
        [OperationContract]
        List<string> GetNotTeacherSubjectsAsync(string email);
        [OperationContract]
        bool TeacherAddNewTeachingSubject(string email, string subject);
        [OperationContract]
        int AddNewSubject(string subject);
        [OperationContract]
        bool SecretaryDeclineClass(string classId);


        [OperationContract]
        List<EditUserInfoBindingModel> GetAllMathTeachers(string type);

        [OperationContract]
        SubjectBindingModel GetSubjectByName(string name);
        [OperationContract]
        TeacherSubjectBindingModel GetTeacherSubjectsAsync(string teacherId);
        [OperationContract]
        List<string> GetAllClassStudents(string classId);
        [OperationContract]
        bool RemoveClassStudents(string students, string classId);
    }
}
