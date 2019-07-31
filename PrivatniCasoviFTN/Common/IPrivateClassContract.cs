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
    public interface IPrivateClassContract
    {
        [OperationContract]
        int AcceptClass(string classId, string email);

        [OperationContract]
        bool TeacherDeleteClass(string classId);

        [OperationContract]
        int JoinClass(string classId,string email);

        [OperationContract]
        bool StudentAddClass(AddPrivateClassBindingModel model, string email);

        [OperationContract]
        bool SecretaryAddClass(AddPrivateClassBindingModel model, string email);

        [OperationContract]
        bool StudentDeclineClass(string email, string classId);

        [OperationContract]
        bool TeacherDeclineClass(string email, string classId);

        [OperationContract]
        List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group);
        [OperationContract]
        string UserChangeDate(string email, string classId, string date,out bool flag);

        [OperationContract]
        int AssignClass(string classId, string teacher);

        [OperationContract]
        bool SecretaryDeclineClass(string classId);

        [OperationContract]
        List<string> GetAllClassStudents(string classId);

        [OperationContract]
        bool RemoveClassStudents(string students, string classId);
    }
}
