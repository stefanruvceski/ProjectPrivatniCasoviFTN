using Common;
using Common.BindingModel;
using Common.BindingModels;
using Common.Database_Models;
using Common.DataBase_Models;
using Common.Utils;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    class UserProvider : IUserContract
    {
        TableHelper subjectHelper = new TableHelper(CLASSES.SUBJECT.ToString());
        TableHelper userHelper = new TableHelper(CLASSES.USER.ToString());
        TableHelper teacherSubjectHelper = new TableHelper(CLASSES.TEACHERSUBJECT.ToString());
        BlobHelper blobHelper = new BlobHelper("userimages");
        public UserProvider() { }
       
        public bool EditUserInformations(EditUserInfoBindingModel bindingModel, string type)
        {
            User model = new User();
            try
            {
                if (bindingModel.Id != "-1")
                    model.RowKey = bindingModel.Id;

                User u = (User)userHelper.GetOne(bindingModel.Email);
                if (u.RowKey != null)
                    model.RowKey = u.RowKey;

                if (!String.IsNullOrWhiteSpace(bindingModel.Image))
                {
                    if (model.RowKey != null)
                    {
                        CloudBlockBlob cloudBlockBlob = blobHelper.UploadStringToBlob(model.RowKey, bindingModel.Image);


                        model = new User(bindingModel.Username, bindingModel.FirstName, bindingModel.LastName, bindingModel.Address, bindingModel.Phone, bindingModel.Email, bindingModel.PrefferEmail, 0, bindingModel.Degree, type, cloudBlockBlob.Name);
                    }
                    else
                    {
                        model = new User(bindingModel.Username, bindingModel.FirstName, bindingModel.LastName, bindingModel.Address, bindingModel.Phone, bindingModel.Email, bindingModel.PrefferEmail, 0, bindingModel.Degree, type, "-1");

                        CloudBlockBlob cloudBlockBlob = blobHelper.UploadStringToBlob(model.RowKey, bindingModel.Image);

                        model.Image = cloudBlockBlob.Name;

                    }
                }
                else
                {
                    model = new User(bindingModel.Username, bindingModel.FirstName, bindingModel.LastName, bindingModel.Address, bindingModel.Phone, bindingModel.Email, bindingModel.PrefferEmail, 0, bindingModel.Degree, type, bindingModel.Image);
                }
                if (bindingModel.Id != "-1")
                    model.RowKey = bindingModel.Id;
                if (u.RowKey != null)
                    model.RowKey = u.RowKey;


                userHelper.AddOrReplace(model);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public EditUserInfoBindingModel GetUserByEmail(string email, string type)
        {
            User u = (User)userHelper.GetOne(email);

            if (u == null)
            {
                u = new User(null, null, null, null, null, email, null, 0, null, type, null);
                userHelper.AddOrReplace(u);
            }

            if (u.Username == null)
            {
                return new EditUserInfoBindingModel() { Email = email };
            }
            else
            {
                string image = "";
                try
                {
                    image = blobHelper.DownloadStringFromBlob(u.Image);
                }
                catch { }
                return new EditUserInfoBindingModel()
                {
                    Id = u.RowKey,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Phone = u.Phone,
                    PrefferEmail = u.PrefferEmail,
                    Degree = u.DegreeOfEducation,
                    Image = image
                };
            }

        }

        public EditUserInfoBindingModel GetUserForEdit(string email)
        {
            User u = (User)userHelper.GetOne(email);

            string image = "";

            try
            {
                image = blobHelper.DownloadStringFromBlob(u.Image);
            }
            catch { }

            return new EditUserInfoBindingModel()
            {
                Id = u.RowKey,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                Phone = u.Phone,
                PrefferEmail = u.PrefferEmail,
                Degree = u.DegreeOfEducation,
                Image = image
            };
        }

        public List<EditUserInfoBindingModel> GetAllMathTeachers(string type)
        {
            List<EditUserInfoBindingModel> retVal = new List<EditUserInfoBindingModel>();

            List<string> mathSubjects = subjectHelper.GetTypeSubjects(type);

            mathSubjects.ForEach(x =>
            {
                teacherSubjectHelper.GetTeachersBySubjectId(int.Parse(x)).ForEach(y => {
                    User u = userHelper.GetUserById(y.ToString());
                    string image = "";
                    try
                    {
                        image = blobHelper.DownloadStringFromBlob(u.Image);
                    }
                    catch { }
                    retVal.Add(new EditUserInfoBindingModel()
                    {
                        Id = u.RowKey,
                        Username = u.Username,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PrefferEmail = u.PrefferEmail,
                        Image = image
                    });
                });


            });



            return retVal.GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
        }

       

        
    }
}
