using Common;
using Common.BindingModel;
using Common.BindingModels;
using Common.DataBase_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    class JobServerProvider : IContract
    {
        TableHelper tableHelper = new TableHelper(CLASSES.SUBJECT.ToString());
        TableHelper tableHelper2 = new TableHelper(CLASSES.COMMENT.ToString());
        TableHelper tableHelper3 = new TableHelper(CLASSES.FIRM.ToString());
        TableHelper tableHelper4 = new TableHelper(CLASSES.PRICELIST.ToString());
        TableHelper tableHelper5 = new TableHelper(CLASSES.CLASS.ToString());
        TableHelper tableHelper6 = new TableHelper(CLASSES.STUDENTCLASS.ToString());
        TableHelper tableHelper7 = new TableHelper(CLASSES.TEACHERCLASS.ToString());
        TableHelper tableHelper8 = new TableHelper(CLASSES.USER.ToString());

        public JobServerProvider()
        {

            //List<CustomEntity> subjects = new List<CustomEntity>()
            //{
            //    new Subject("PJISP"),
            //    new Subject("SCADA"),
            //    new Subject("RVA"),
            //    new Subject("MISS"),
            //    new Subject("HCI"),
            //};

            //List<CustomEntity> comments = new List<CustomEntity>()
            //{
            //    new Comment("Super cas",5),
            //    new Comment("ok cas",4),
            //    new Comment("pristojan cas",3),
            //    new Comment("los cas",2),
            //    new Comment("dosta los cas",1),
            //};

            //List<CustomEntity> firms = new List<CustomEntity>()
            //{
            //    new Firm("Privatni Casovi FTN","0658601731","Alekse Santica 46","privatnicasoviFTN@privatnicasovi.onmicrosoft.com","Novi Sad"),
            //};

            //List<CustomEntity> pricelists = new List<CustomEntity>()
            //{
            //    new Pricelist(1500,1,0),
            //    new Pricelist(2000,2,0),
            //    new Pricelist(1000,3,0),
            //    new Pricelist(1500,4,0),
            //};

            ////List<CustomEntity> users = new List<CustomEntity>()
            ////{
            ////    new User("filipr","Filip","Ruvceski","Kolo srpskih sestara 22","065123123","filipruvceski@privatnicasovi.onmicrosoft.com","filipruvceski@privatnicasovi.onmicrosoft.com",0,"Osnovna skola"),
            ////     new User("milicap","Milica","Pranjkic","Narodnog Fronta 45","063123123","milicapranjkic@privatnicasovi.onmicrosoft.com","milicapranjkic@privatnicasovi.onmicrosoft.com",0,"srednja skola"),
            ////};

            List<CustomEntity> classes = new List<CustomEntity>()
            {
                new PrivateClass("Kola",0,1500,DateTime.Now,3,1),
            };

            List<CustomEntity> studentclasses = new List<CustomEntity>()
            {
                new StudentClass(0,0,0,false),
            };

            //List<CustomEntity> teacherClasses = new List<CustomEntity>()
            //{
            //    new TeacherClass(1,0,1,false),
            //};

            //tableHelper.InitTable(subjects);
            //tableHelper2.InitTable(comments);
            //tableHelper3.InitTable(firms);
            ////tableHelper8.InitTable(users);
            //tableHelper4.InitTable(pricelists);
            //tableHelper5.InitTable(classes);
            //tableHelper6.InitTable(studentclasses);
            //tableHelper7.InitTable(teacherClasses);
            //Trace.WriteLine("USPEO");


        }

        public bool EditUserInformations(EditUserInfoBindingModel bindingModel)
        {
            try
            {
                User model = new User(bindingModel.Username, bindingModel.FirstName, bindingModel.LastName, bindingModel.Address, bindingModel.Phone, bindingModel.Email, bindingModel.PrefferEmail, 0, bindingModel.Degree);
                if (bindingModel.Id != "-1")
                    model.RowKey = bindingModel.Id;
                User u = (User)tableHelper8.GetOne(bindingModel.Email);
                if (u.RowKey != null)
                    model.RowKey = u.RowKey;

                tableHelper8.AddOrReplace(model);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public EditUserInfoBindingModel GetUserByEmail(string email)
        {
            User u = (User)tableHelper8.GetOne(email);

            if(u.Email == null)
            {
                u = new User(null, null, null, null, null, email,null, 0, null);
                tableHelper8.AddOrReplace(u);
            }

            if (u.Username == null)
            {
                return new EditUserInfoBindingModel() { Email = email };
            }
            else
            {
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
                    Degree = u.DegreeOfEducation
                };
            }

        }

        public EditUserInfoBindingModel GetUserForEdit(string email)
        {
            User u = (User)tableHelper8.GetOne(email);

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
                Degree = u.DegreeOfEducation
            };
        }

        public List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group)
        {
            List<PrivateClassBindingModel> retVal = new List<PrivateClassBindingModel>();
            retVal =  tableHelper5.GetUsersClasses(tableHelper8.GetUsetId(email),group);

            foreach (PrivateClassBindingModel item in retVal)
            {
                DateTime dt = DateTime.Parse(item.StartDate);//2019-07-04T10:30:00',
                string day = dt.Day.ToString();
                if (dt.Day < 10)
                    day = "0" + day;

                string month = dt.Month.ToString();
                if (dt.Month < 10)
                    month = "0" + month;

                item.StartDate = $"{dt.Year}-{month}-{day}T{dt.TimeOfDay.ToString()}";
                item.EndDate = $"{dt.Year}-{month}-{day}T{(dt.TimeOfDay+ new TimeSpan(1,30,0)).ToString()}";
                if(item.IsMine == "yes")
                {
                    item.Color = "#f5d142";
                }
                else if (item.Status == CLASS_STATUS.ACCEPTED.ToString())
                {
                    item.Color = "#288010";
                }
                else if(item.Status == CLASS_STATUS.REQUESTED.ToString())
                {
                    item.Color = "#4287f5";
                }
                else
                {
                    item.Color = "#f54242";
                }
            }
            
            return retVal;
        }

        public bool AcceptClass(string classId, string email)
        {
            return tableHelper5.AcceptClass(tableHelper8.GetUsetId(email), classId);
        }

        public bool TeacherDeleteClass(string classId)
        {
            return tableHelper5.TeacherDeleteClass(classId);
        }

        public int JoinClass(string classId, string email)
        {
            return tableHelper5.StudentJoinClass(tableHelper8.GetUsetId(email), classId);
        }

        public List<string> GetAllSubjects()
        {
            return tableHelper.GetAllSubjects();
        }

        public bool AddClass(AddPrivateClassBindingModel model, string email)
        {
            //mesec/dan/godina
            DateTime dt = new DateTime(int.Parse(model.Date.Split('/')[2]), int.Parse(model.Date.Split('/')[0]), int.Parse(model.Date.Split('/')[1]), int.Parse(model.Time.Split(':')[0]), int.Parse(model.Time.Split(':')[1]),0);
            PrivateClass privateClass = new PrivateClass(model.Lesson,0,0,dt,0,1);
            
            return tableHelper5.AddClass(model,privateClass, tableHelper8.GetUsetId(email));
        }

        public bool UserDeclineClass(string email, string classId)
        {
            return tableHelper5.UserDeclineClass(tableHelper8.GetUsetId(email), classId);
        }

        public string UserChangeDate(string email, string classId, string date,out bool flag)
        {
            string time = date.Split(',')[1];
            date = date.Split(',')[0];
            
            DateTime dt = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0);

            string retVal =  tableHelper5.UserChangeDate(tableHelper8.GetUsetId(email), classId,dt);

            DateTime dt2 = DateTime.Parse(retVal.Split('_')[1]);
            string day = dt2.Day.ToString();
            if (dt2.Day < 10)
                day = "0" + day;

            string month = dt2.Month.ToString();
            if (dt2.Month < 10)
                month = "0" + month;
            if (retVal.Split('_')[0] == "success")
                flag = true;
            else
                flag = false;
            return retVal.Split('_')[0]+ "_"+ $"{dt2.Year}-{month}-{day}T{dt2.TimeOfDay.ToString()}";
        }
    }
}
