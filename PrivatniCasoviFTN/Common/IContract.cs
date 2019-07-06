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
        bool EditUserInformations(EditUserInfoBindingModel bindingModel);
        [OperationContract]
        EditUserInfoBindingModel GetUserByEmail(string email);
        [OperationContract]
        EditUserInfoBindingModel GetUserForEdit(string email);
        [OperationContract]
        List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group);
        [OperationContract]
        bool AcceptClass(string classId, string email);
        [OperationContract]
        bool TeacherDeleteClass(string classId);
        [OperationContract]
        int JoinClass(string classId,string email);

        [OperationContract]
        List<string> GetAllSubjects();
        [OperationContract]
        bool AddClass(AddPrivateClassBindingModel model, string email);
        [OperationContract]
        bool UserDeclineClass(string email, string classId);
        [OperationContract]
        string UserChangeDate(string email, string classId, string date,out bool flag);
    }
}
