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
        TableHelper tableHelper9 = new TableHelper(CLASSES.TEACHERSUBJECT.ToString());

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

            //List<CustomEntity> classes = new List<CustomEntity>()
            //{
            //    new PrivateClass("Kola",0,1500,DateTime.Now,3,1),
            //};

            //List<CustomEntity> teacherSubjects = new List<CustomEntity>()
            //{
            //    new TeacherSubject(1, 1),
            //    new TeacherSubject(1, 2),
            //    new TeacherSubject(1,3),
            //};

            //List<CustomEntity> studentclasses = new List<CustomEntity>()
            //{
            //    new StudentClass(0,0,0,false),
            //};

            //List<CustomEntity> teacherClasses = new List<CustomEntity>()
            //{
            //    new TeacherClass(1,0,1,false),
            //};
            //  new TableHelper(CLASSES.TEACHERSUBJECT.ToString()).InitTable(teacherSubjects);
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
        BlobHelper blobHelper = new BlobHelper("userimages");
        public bool EditUserInformations(EditUserInfoBindingModel bindingModel, string type)
        {
            User model = new User();
            try
            {
                if (bindingModel.Id != "-1")
                    model.RowKey = bindingModel.Id;

                User u = (User)tableHelper8.GetOne(bindingModel.Email);
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


                tableHelper8.AddOrReplace(model);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public EditUserInfoBindingModel GetUserByEmail(string email, string type)
        {
            User u = (User)tableHelper8.GetOne(email);

            if (u == null)
            {
                u = new User(null, null, null, null, null, email, null, 0, null, type, null);
                tableHelper8.AddOrReplace(u);
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
            User u = (User)tableHelper8.GetOne(email);

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

        public List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group)
        {
            List<PrivateClassBindingModel> retVal = new List<PrivateClassBindingModel>();
            retVal = tableHelper5.GetUsersClasses(tableHelper8.GetUsetId(email), group);

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
                item.EndDate = $"{dt.Year}-{month}-{day}T{(dt.TimeOfDay + new TimeSpan(1, 30, 0)).ToString()}";
                if (item.IsMine == "yes")
                {
                    item.Color = "#f5d142";
                }
                else if (item.Status == CLASS_STATUS.ACCEPTED.ToString())
                {
                    item.Color = "#288010";
                }
                else if (item.Status == CLASS_STATUS.REQUESTED.ToString())
                {
                    item.Color = "#4287f5";
                }
                else
                {
                    item.Color = "#f54242";
                }
                if (item.Status == CLASS_STATUS.DECLINED.ToString())
                {
                    item.Color = "#f54242";
                }
            }

            return retVal;
        }

        public int AcceptClass(string classId, string email)
        {
            int flag = tableHelper5.AcceptClass(tableHelper8.GetUsetId(email), classId);

            if (flag == 1)
            {
                List<User> students = tableHelper6.GetClassStudents(classId);
                User teacher = (User)tableHelper8.GetOne(email);
                PrivateClass privateClass = (PrivateClass)tableHelper5.GetOne(classId);
                Subject subject = (Subject)tableHelper.GetOne(privateClass.SubjectId.ToString());
                string studentsUsername = "";
                foreach (User item in students)
                {
                    studentsUsername += item.Username + ", ";
                    if (item.PrefferEmail != null)
                    {
                        MailHandler.SendMail(item.PrefferEmail, "Class accepted", $"Your class has been accepted by {teacher.Username}.");
                    }
                }

                if (teacher.PrefferEmail != null)
                {
                    MailHandler.SendMail(teacher.PrefferEmail, "Class accepted", $"You accepted class {subject.Name}\nLesson {privateClass.Lesson}\nDate {privateClass.Date}\nStudents: {studentsUsername}");
                }

            }

            return flag;
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

        public bool StudentAddClass(AddPrivateClassBindingModel model, string email)
        {
            //mesec/dan/godina
            int hour = int.Parse(model.Time.Split(':')[0]);
            if (hour == 22)
                hour = 0;
            else if (hour == 23)
                hour = 1;
            else
                hour += 2;
            DateTime dt = new DateTime(int.Parse(model.Date.Split('/')[2]), int.Parse(model.Date.Split('/')[0]), int.Parse(model.Date.Split('/')[1]), hour, int.Parse(model.Time.Split(':')[1]), 0);
            PrivateClass privateClass = new PrivateClass(model.Lesson, 0, 0, dt, 0, 1);

            return tableHelper5.StudentAddClass(model, privateClass, tableHelper8.GetUsetId(email));
        }

        public bool SecretaryAddClass(AddPrivateClassBindingModel model, string email)
        {
            //mesec/dan/godina
            DateTime dt = new DateTime(int.Parse(model.Date.Split('/')[2]), int.Parse(model.Date.Split('/')[0]), int.Parse(model.Date.Split('/')[1]), int.Parse(model.Time.Split(':')[0]), int.Parse(model.Time.Split(':')[1]), 0);
            dt = dt + new TimeSpan(2, 0, 0);
            PrivateClass privateClass = new PrivateClass(model.Lesson, 0, 0, dt, 0, int.Parse(model.NumOfStudents));

            return tableHelper5.SecretaryAddClass(model, privateClass);
        }

        public bool StudentDeclineClass(string email, string classId)
        {
            int flag = tableHelper5.StudentDeclineClass(tableHelper8.GetUsetId(email), classId);

            if (flag == -1)
            {
                return false;
            }
            else
            {
                if (flag == -2)
                {
                    PrivateClass privateClass = (PrivateClass)tableHelper5.GetOne(classId);
                    User teacher = tableHelper7.GetClassTeacher(classId);

                    MailHandler.SendMail(teacher.PrefferEmail, "Class DECLINED by students", $"Class {privateClass.Lesson} at {privateClass.Date} has been DECLINED by students.");
                }

                return true;
            }
        }

        public bool TeacherDeclineClass(string email, string classId)
        {
            if (tableHelper5.TeacherDeclineClass(tableHelper8.GetUsetId(email), classId))
            {
                PrivateClass privateClass = (PrivateClass)tableHelper5.GetOne(classId);
                tableHelper6.GetClassStudents(classId).ForEach(x =>
                {
                    MailHandler.SendMail(x.PrefferEmail, "Class DECLINED by teacher", $"Class {privateClass.Lesson} at {privateClass.Date} has been DECLINED by teacher.");
                });

                return true;
            }
            return false;
        }

        public bool SecretaryDeclineClass(string classId)
        {
            try
            {
                tableHelper7.SecretaryDeclineClass(classId);


                return true;
            }
            catch
            {
                return false;
            }

        }

        public string UserChangeDate(string email, string classId, string date, out bool flag)
        {
            string time = date.Split(',')[1];
            date = date.Split(',')[0];

            DateTime dt = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0);

            string retVal = tableHelper5.UserChangeDate(tableHelper8.GetUsetId(email), classId, dt);

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
            return retVal.Split('_')[0] + "_" + $"{dt2.Year}-{month}-{day}T{dt2.TimeOfDay.ToString()}";
        }

        public List<string> GetSubjectTeachers(string subject)
        {
            return tableHelper.GetSubjectTeachers(subject);
        }

        public int AssignClass(string classId, string teacher)
        {
            return tableHelper5.AcceptClass(tableHelper8.GetIdByUsername(teacher), classId);
        }

        public List<string> GetNotTeacherSubjectsAsync(string email)
        {
            User teacher = (User)tableHelper8.GetOne(email);

            return tableHelper.GetNotTeacherSubjectsAsync(teacher.RowKey);
        }

        public bool TeacherAddNewTeachingSubject(string email, string subject)
        {
            try
            {
                User teacher = (User)tableHelper8.GetOne(email);

                int subjectId = tableHelper.GetSubjectId(subject);

                TeacherSubject teacherSubject = new TeacherSubject(int.Parse(teacher.RowKey), subjectId);


                tableHelper9.AddOrReplace(teacherSubject);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int AddNewSubject(string subject)
        {
            try
            {
                subject = subject.ToUpper();
                if (!tableHelper.GetAllSubjects().Contains(subject)) {

                    string type = "";

                    if (Dictionaries.Programming.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Programming[subject.ToUpper()];
                    else if (Dictionaries.Mathematics.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Mathematics[subject.ToUpper()];
                    else if (Dictionaries.Electrotehnics.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Electrotehnics[subject.ToUpper()];

                    Subject newSubject = new Subject(subject, type, subject);
                    Pricelist pricelist = new Pricelist(1500, int.Parse(newSubject.RowKey), 0);
                    new TableHelper(CLASSES.PRICELIST.ToString()).AddOrReplace(pricelist);
                    tableHelper.AddOrReplace(newSubject);
                    tableHelper8.GetAllTeachers().ForEach(x =>
                    {
                        MailHandler.SendMail(x.PrefferEmail, "New Subject added", $"New Subject {subject} has been added, if you want to teach it log in and assign it.");
                    });
                    return 1;
                }
                return -1;
            }
            catch(Exception e)
            {
                return -2;
            }
        }

        public List<EditUserInfoBindingModel> GetAllMathTeachers(string type)
        {
            List<EditUserInfoBindingModel> retVal = new List<EditUserInfoBindingModel>();

            List<string> mathSubjects = tableHelper.GetTypeSubjects(type);

            mathSubjects.ForEach(x =>
            {
                tableHelper9.GetTeachersBySubjectId(int.Parse(x)).ForEach(y => {
                    User u = tableHelper8.GetUserById(y.ToString());
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

        public SubjectBindingModel GetSubjectByName(string name)
        {
            SubjectBindingModel retVal = new SubjectBindingModel();
            retVal.Teachers = new List<SubjectTeacherBindingModel>();
            try
            {
                Subject subject = tableHelper.GetSubjectByName(name);


                retVal.Name = subject.Name;
                retVal.Details = subject.Details;
                tableHelper9.GetTeachersBySubjectId(int.Parse(subject.RowKey)).ForEach(x =>
                {
                    User u = tableHelper8.GetUserById(x.ToString());
                    string image = "";
                    try
                    {
                        image = blobHelper.DownloadStringFromBlob(u.Image);
                    }
                    catch { }

                    retVal.Teachers.Add(new SubjectTeacherBindingModel()
                    {
                        FullName = u.FirstName + " " + u.LastName,
                        Email = u.Email,
                        Image = image
                    });
                });
            }
            catch { }
            return retVal;
        }

        public TeacherSubjectBindingModel GetTeacherSubjectsAsync(string teacherId)
        {
            TeacherSubjectBindingModel retVal = new TeacherSubjectBindingModel() { Subjects = new List<SubjectByType>() };

            SubjectByType mathematics = new SubjectByType() { Subjects = new List<SubjectTeach>() ,Name = "MATHEMATICS" };
            SubjectByType electrotehnics = new SubjectByType() { Subjects = new List<SubjectTeach>(),Name= "ELECTROTEHNICS" };
            SubjectByType programming = new SubjectByType() { Subjects = new List<SubjectTeach>(),Name= "PROGRAMMING" };


            tableHelper.GetTeacherSubjectsById(teacherId).ForEach(x => {
                if(x.Type == "MATHEMATICS")
                    mathematics.Subjects.Add(new SubjectTeach()
                    {
                         Name = x.Name,
                         Type = x.Type,
                         Details = x.Details

                    });
                else if (x.Type == "ELECTROTEHNICS")
                    electrotehnics.Subjects.Add(new SubjectTeach()
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Details = x.Details

                    });
                else if (x.Type == "PROGRAMMING")
                    programming.Subjects.Add(new SubjectTeach()
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Details = x.Details

                    });

            });

            if (mathematics.Subjects.Count != 0)
                retVal.Subjects.Add(mathematics);
            if (electrotehnics.Subjects.Count != 0)
                retVal.Subjects.Add(electrotehnics);
            if (programming.Subjects.Count != 0)
                retVal.Subjects.Add(programming);

            return retVal;
        }

        public List<string> GetAllClassStudents(string classId)
        {
            List<string> retVal = new List<string>();

            List<User> students = tableHelper6.GetClassStudents(classId);

            students.ForEach(item =>
            {
                retVal.Add(item.Username);
            });

            return retVal;
        }

        public bool RemoveClassStudents(string students, string classId)
        {
            try
            {
                students.Split('_').ToList().ForEach(student =>
                {
                    if (student != "")
                    {
                        string studentId = tableHelper8.GetIdByUsername(student);
                        tableHelper6.Delete(tableHelper6.GetStudentClass(studentId, classId));
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
