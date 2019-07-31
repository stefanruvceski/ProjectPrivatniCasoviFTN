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
    public interface IUserContract
    {
        [OperationContract]
        bool EditUserInformations(EditUserInfoBindingModel bindingModel, string type);

        [OperationContract]
        EditUserInfoBindingModel GetUserByEmail(string email, string type);

        [OperationContract]
        EditUserInfoBindingModel GetUserForEdit(string email);

        [OperationContract]
        List<EditUserInfoBindingModel> GetAllMathTeachers(string type);
    }
}
