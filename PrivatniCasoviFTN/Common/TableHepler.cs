using Common.BindingModel;
using Common.BindingModels;
using Common.Database_Models;
using Common.DataBase_Models;
using Common.Utils;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TableHelper
    {
        #region Fields
        CloudStorageAccount storageAccount;
        public CloudTable table;
        CloudTableClient tableClient;
        CLASSES _class;
        #endregion

        #region Kreiranje tabele
        // Kreiranje tabele
        public TableHelper(string tableName)
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            tableClient = new CloudTableClient(new Uri(storageAccount.TableEndpoint.AbsoluteUri),
                                                                storageAccount.Credentials);
            table = tableClient.GetTableReference(tableName);
            if (table.CreateIfNotExists())
            {
                // InitTable();
            }
            Enum.TryParse(tableName, out _class);
        }
        #endregion

        public void InitTable(List<CustomEntity> list)
        {
            TableBatchOperation tableOperations = new TableBatchOperation();

            switch (_class)
            {
                case CLASSES.SUBJECT:
                    {
                        List<Subject> entities = new List<Subject>();
                        list.ForEach(x => entities.Add((Subject)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.CLASS:
                    {
                        List<PrivateClass> entities = new List<PrivateClass>();
                        list.ForEach(x => entities.Add((PrivateClass)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.FIRM:
                    {
                        List<Firm> entities = new List<Firm>();
                        list.ForEach(x => entities.Add((Firm)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.USER:
                    {
                        List<User> entities = new List<User>();
                        list.ForEach(x => entities.Add((User)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.COMMENT:
                    {
                        List<Comment> entities = new List<Comment>();
                        list.ForEach(x => entities.Add((Comment)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.PRICELIST:
                    {
                        List<Pricelist> entities = new List<Pricelist>();
                        list.ForEach(x => entities.Add((Pricelist)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.STUDENTCLASS:
                    {
                        List<StudentClass> entities = new List<StudentClass>();
                        list.ForEach(x => entities.Add((StudentClass)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.TEACHERCLASS:
                    {
                        List<TeacherClass> entities = new List<TeacherClass>();
                        list.ForEach(x => entities.Add((TeacherClass)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                case CLASSES.TEACHERSUBJECT:
                    {
                        List<TeacherSubject> entities = new List<TeacherSubject>();
                        list.ForEach(x => entities.Add((TeacherSubject)x));

                        entities.ForEach(x => tableOperations.InsertOrReplace(x));
                    }
                    break;
                default:
                    break;
            }




            table.ExecuteBatch(tableOperations);
        }

        #region Operacije nad tabelom
        public void AddOrReplace(CustomEntity entity)
        {

            if (_class.Equals(CLASSES.SUBJECT))
            {
                TableOperation add = TableOperation.InsertOrReplace((Subject)entity);
                table.Execute(add);
            }
            else if (_class.Equals(CLASSES.USER))
            {
                TableOperation add = TableOperation.InsertOrReplace((User)entity);
                table.Execute(add);
            }
            else if (_class.Equals(CLASSES.TEACHERCLASS))
            {
                TableOperation add = TableOperation.InsertOrReplace((TeacherClass)entity);
                table.Execute(add);
            }
            else if (_class.Equals(CLASSES.CLASS))
            {
                TableOperation add = TableOperation.InsertOrReplace((PrivateClass)entity);
                table.Execute(add);
            }
            else if (_class.Equals(CLASSES.STUDENTCLASS))
            {
                TableOperation add = TableOperation.InsertOrReplace((StudentClass)entity);
                table.Execute(add);
            }
            else if (_class.Equals(CLASSES.TEACHERSUBJECT))
            {
                TableOperation add = TableOperation.InsertOrReplace((TeacherSubject)entity);
                table.Execute(add);
            }
        }

        public void Delete(CustomEntity entity)
        {

            if (_class.Equals(CLASSES.SUBJECT))
            {
                TableOperation delete = TableOperation.Delete((Subject)entity);
                table.Execute(delete);
            }
            else if (_class.Equals(CLASSES.STUDENTCLASS))
            {
                TableOperation delete = TableOperation.Delete((StudentClass)entity);
                table.Execute(delete);
            }
            else if (_class.Equals(CLASSES.TEACHERCLASS))
            {
                TableOperation delete = TableOperation.Delete((TeacherClass)entity);
                table.Execute(delete);
            }
        }

        public int GetCount()
        {
            int retVal = 0;

            if (_class.Equals(CLASSES.SUBJECT))
            {
                IQueryable<Subject> requests = from g in table.CreateQuery<Subject>()
                                               where g.PartitionKey == _class.ToString()
                                               select g;

                retVal = requests.ToList().Count();
            }

            return retVal;
        }

        public User GetUserById(string id)
        {
            IQueryable<User> requests = from g in table.CreateQuery<User>()
                                        where g.PartitionKey == _class.ToString() && g.RowKey == id
                                        select g;

            return requests.ToList()[0];
        }

        public CustomEntity GetOne(string id)
        {
            if (_class.Equals(CLASSES.SUBJECT))
            {
                IQueryable<Subject> requests = from g in table.CreateQuery<Subject>()
                                               where g.PartitionKey == _class.ToString() && g.RowKey == id
                                               select g;

                return requests.ToList()[0];
            }
            if (_class.Equals(CLASSES.USER))
            {
                try
                {
                    IQueryable<User> requests = from g in table.CreateQuery<User>()
                                                where g.PartitionKey == _class.ToString() && g.Email == id
                                                select g;

                    return requests.ToList()[0];
                }
                catch
                {
                    return new User();
                }
            }
            if (_class.Equals(CLASSES.CLASS))
            {
                IQueryable<PrivateClass> requests = from g in table.CreateQuery<PrivateClass>()
                                                    where g.PartitionKey == _class.ToString() && g.RowKey == id
                                                    select g;

                return requests.ToList()[0];
            }

            return null;
        }

        public List<int> GetTeacherSubjects(string teacherId)
        {
            IQueryable<int> requests = from g in table.CreateQuery<TeacherSubject>()
                                       where g.PartitionKey == _class.ToString() && g.TeachertId == int.Parse(teacherId)
                                       select g.SubjectId;

            return requests.ToList();
        }

        public List<User> GetClassStudents(string classId)
        {
            IQueryable<int> requests = from g in table.CreateQuery<StudentClass>()
                                       where g.PartitionKey == _class.ToString() && g.ClassId == int.Parse(classId)
                                       select g.StudentId;

            List<User> users = new List<User>();

            requests.ToList().ForEach(x => users.Add(new TableHelper(CLASSES.USER.ToString()).GetUserById(x.ToString())));

            return users;
        }

        public int AcceptClass(string userId, string classId)
        {
            if (_class.Equals(CLASSES.CLASS))
            {

                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                privateClass.ClassStatus = CLASS_STATUS.ACCEPTED.ToString();
                if (!new TableHelper(CLASSES.TEACHERSUBJECT.ToString()).GetTeacherSubjects(userId).Contains(privateClass.SubjectId))
                    return -2;

                TableOperation replace = TableOperation.Replace(privateClass);
                table.Execute(replace);

                TeacherClass teacherClass = new TeacherClass(int.Parse(userId), int.Parse(classId), -1, false);
                new TableHelper(CLASSES.TEACHERCLASS.ToString()).AddOrReplace(teacherClass);
                return 1;
            }
            return -1;
        }

        public string GetUsetId(string email)
        {
            var request = from g in table.CreateQuery<User>()
                          where g.PartitionKey == _class.ToString() && g.Email == email
                          select g.RowKey;

            return request.ToList()[0].ToString();
        }
        public bool TeacherDeleteClass(string classId)
        {
            PrivateClass privateClass = (PrivateClass)GetOne(classId);
            privateClass.ClassStatus = CLASS_STATUS.DECLINED.ToString();
            TableOperation replace = TableOperation.Replace(privateClass);
            table.Execute(replace);
            return true;
        }
        public dynamic GetUsersClasses(string id, string group)
        {
            if (_class.Equals(CLASSES.CLASS))
            {
                TableHelper tsc = new TableHelper(CLASSES.STUDENTCLASS.ToString());
                TableHelper ts = new TableHelper(CLASSES.SUBJECT.ToString());
                TableHelper ttc = new TableHelper(CLASSES.TEACHERCLASS.ToString());
                TableHelper tt = new TableHelper(CLASSES.USER.ToString());
                if (group == "PrivatniCasoviStudents")
                {

                    var myrequests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                     join sc in tsc.table.CreateQuery<StudentClass>().ToList() on pc.RowKey equals sc.ClassId.ToString()
                                     where sc.StudentId == int.Parse(id)
                                     select new PrivateClassBindingModel { Id = pc.RowKey, Subject = "", Teacher = "", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };
                    var requests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                   join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey

                                   select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = "No techer yet", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };


                    Dictionary<string, PrivateClassBindingModel> dic = new Dictionary<string, PrivateClassBindingModel>();


                    var requests2 = from pc in table.CreateQuery<PrivateClass>().ToList()
                                    join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey
                                    join tc in ttc.table.CreateQuery<TeacherClass>().ToList() on pc.RowKey equals tc.ClassId.ToString()
                                    join t in tt.table.CreateQuery<User>().ToList() on tc.TeachertId.ToString() equals t.RowKey
                                    select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = t.Username, Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };

                    requests2.ToList().GroupBy(customer => customer.Id).Select(g => g.First()).ToList().ForEach(x => dic.Add(x.Id, x));
                    List<PrivateClassBindingModel> retVal = requests.ToList();
                    Dictionary<string, PrivateClassBindingModel> myDic = new Dictionary<string, PrivateClassBindingModel>();
                    myrequests.ToList().ForEach(x => myDic.Add(x.Id, x));
                    for (int i = 0; i < retVal.Count; i++)
                    {
                        if (dic.ContainsKey(retVal[i].Id))
                            retVal[i].Teacher = dic[retVal[i].Id].Teacher;

                        if (myDic.ContainsKey(retVal[i].Id))
                            retVal[i].IsMine = "yes";
                        else
                            retVal[i].IsMine = "no";
                    }
                    return retVal.GroupBy(customer => customer.Id).Select(g => g.First()).ToList();

                }
                else if (group == "PrivatniCasoviTeachers")
                {
                    var myrequests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                     join tc in ttc.table.CreateQuery<TeacherClass>().ToList() on pc.RowKey equals tc.ClassId.ToString()
                                     where tc.TeachertId == int.Parse(id)
                                     select new PrivateClassBindingModel { Id = pc.RowKey, Subject = "", Teacher = "", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };


                    var requests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                   join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey
                                   where pc.ClassStatus == CLASS_STATUS.REQUESTED.ToString() || pc.ClassStatus == CLASS_STATUS.ACCEPTED.ToString()
                                   select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = "", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };

                    List<PrivateClassBindingModel> retVal = requests.ToList().GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
                    Dictionary<string, PrivateClassBindingModel> myDic = new Dictionary<string, PrivateClassBindingModel>();
                    myrequests.ToList().ForEach(x => myDic.Add(x.Id, x));
                    for (int i = 0; i < retVal.Count; i++)
                    {


                        if (myDic.ContainsKey(retVal[i].Id))
                            retVal[i].IsMine = "yes";
                        else
                            retVal[i].IsMine = "no";
                    }
                    return retVal.GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
                }
                else
                {
                    var requests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                   join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey

                                   select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = "", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };


                    requests = requests.GroupBy(customer => customer.Id).Select(g => g.First()).ToList();

                    var requests2 = from pc in table.CreateQuery<PrivateClass>().ToList()
                                    join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey
                                    join tc in ttc.table.CreateQuery<TeacherClass>().ToList() on pc.RowKey equals tc.ClassId.ToString()
                                    join t in tt.table.CreateQuery<User>().ToList() on tc.TeachertId.ToString() equals t.RowKey
                                    select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = t.Username, Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };

                    Dictionary<string, PrivateClassBindingModel> dic = new Dictionary<string, PrivateClassBindingModel>();
                    requests2.ToList().GroupBy(customer => customer.Id).Select(g => g.First()).ToList().ForEach(x => dic.Add(x.Id, x));
                    List<PrivateClassBindingModel> retVal = requests.ToList();

                    for (int i = 0; i < retVal.Count; i++)
                    {
                        if (dic.ContainsKey(retVal[i].Id))
                            retVal[i].Teacher = dic[retVal[i].Id].Teacher;

                    }
                    return retVal.GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
                }
            }
            return null;
        }

        public List<string> GetSubjectTeachers(string subject)
        {
            IQueryable<string> requests = from g in table.CreateQuery<Subject>()
                                          where g.PartitionKey == _class.ToString() && g.Name == subject
                                          select g.RowKey;

            string subjectId = requests.ToList()[0];

            IQueryable<int> requests2 = from g in new TableHelper(CLASSES.TEACHERSUBJECT.ToString()).table.CreateQuery<TeacherSubject>()
                                        where g.PartitionKey == new TableHelper(CLASSES.TEACHERSUBJECT.ToString())._class.ToString() && g.SubjectId == int.Parse(subjectId)
                                        select g.TeachertId;
            List<string> retVal = new List<string>();
            requests2.ToList().ForEach(x => retVal.Add(new TableHelper(CLASSES.USER.ToString()).GetUserById(x.ToString()).Username));

            return retVal;


        }
        public List<string> GetSubjectTeachersEmail(string subject)
        {
            ;

            IQueryable<int> requests2 = from g in new TableHelper(CLASSES.TEACHERSUBJECT.ToString()).table.CreateQuery<TeacherSubject>()
                                        where g.PartitionKey == new TableHelper(CLASSES.TEACHERSUBJECT.ToString())._class.ToString() && g.SubjectId == int.Parse(subject)
                                        select g.TeachertId;
            List<string> retVal = new List<string>();
            requests2.ToList().ForEach(x => retVal.Add(new TableHelper(CLASSES.USER.ToString()).GetUserById(x.ToString()).PrefferEmail));

            return retVal;


        }

        public string GetIdByUsername(string username)
        {
            IQueryable<string> requests = from g in table.CreateQuery<User>()
                                          where g.PartitionKey == _class.ToString() && g.Username == username
                                          select g.RowKey;

            return requests.ToList()[0];
        }

        private StudentClass GetStudentClass(string userId, string classId)
        {
            IQueryable<StudentClass> requests = from g in table.CreateQuery<StudentClass>()
                                                where g.PartitionKey == _class.ToString() && g.StudentId == int.Parse(userId) && g.ClassId == int.Parse(classId)
                                                select g;

            return requests.ToList()[0];
        }

        private TeacherClass GetTeacherClass(string userId, string classId)
        {
            IQueryable<TeacherClass> requests = from g in table.CreateQuery<TeacherClass>()
                                                where g.PartitionKey == _class.ToString() && g.TeachertId == int.Parse(userId) && g.ClassId == int.Parse(classId)
                                                select g;

            return requests.ToList()[0];
        }

        private int GetStudentClassCount(string userId, string classId)
        {
            IQueryable<StudentClass> requests = from g in table.CreateQuery<StudentClass>()
                                                where g.PartitionKey == _class.ToString() && g.StudentId == int.Parse(userId) && g.ClassId == int.Parse(classId)
                                                select g;

            return requests.ToList().Count;
        }
        public string UserChangeDate(string userId, string classId, DateTime dateTime)
        {
            string retval = $"success_{dateTime}";
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                if (privateClass.NumberOfStudents != 1)
                {
                    return $"There are more students in this class,you cant move it_{privateClass.Date}";
                }
                if (new TableHelper(CLASSES.STUDENTCLASS.ToString()).GetStudentClassCount(userId, classId) != 1)
                {
                    return $"You are not in this class,you cant move it_{privateClass.Date}";
                }
                privateClass.Date = dateTime;
                privateClass.Date += new TimeSpan(2, 0, 0);
                AddOrReplace(privateClass);

            }
            catch
            {
                return "Inner error";
            }



            return retval;
        }
        public int StudentDeclineClass(string userId, string classId)
        {
            int retVal = 1;
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                privateClass.NumberOfStudents--;
                if (privateClass.NumberOfStudents == 0)
                {
                    if (privateClass.ClassStatus == CLASS_STATUS.ACCEPTED.ToString())
                    {
                        privateClass.ClassStatus = CLASS_STATUS.DECLINED.ToString();
                        retVal = -2;
                    }
                    else
                    {
                        privateClass.ClassStatus = CLASS_STATUS.DECLINED.ToString();
                        retVal = 1;
                    }

                }

                AddOrReplace(privateClass);
                StudentClass studentClass = new TableHelper(CLASSES.STUDENTCLASS.ToString()).GetStudentClass(userId, classId);
                new TableHelper(CLASSES.STUDENTCLASS.ToString()).Delete(studentClass);


            }
            catch
            {
                retVal = -1;
            }
            return retVal;
        }

        public User GetClassTeacher(string classId)
        {
            try
            {


                IQueryable<int> teacher = from g in table.CreateQuery<TeacherClass>()
                                          where g.PartitionKey == _class.ToString() && g.ClassId == int.Parse(classId)
                                          select g.TeachertId;

                TableHelper users = new TableHelper(CLASSES.USER.ToString());

                IQueryable<User> requests = from g in users.table.CreateQuery<User>()
                                            where g.PartitionKey == users._class.ToString() && g.RowKey == teacher.ToList()[0].ToString()
                                            select g;

                return requests.ToList()[0];
            }
            catch
            {
                return null;
            }

        }

        public bool TeacherDeclineClass(string userId, string classId)
        {
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                privateClass.ClassStatus = CLASS_STATUS.REQUESTED.ToString();

                AddOrReplace(privateClass);
                TeacherClass teacherClass = new TableHelper(CLASSES.TEACHERCLASS.ToString()).GetTeacherClass(userId, classId);
                new TableHelper(CLASSES.TEACHERCLASS.ToString()).Delete(teacherClass);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAllTeachers()
        {
            IQueryable<User> requests = from g in table.CreateQuery<User>()
                                        where g.PartitionKey == _class.ToString() && g.Type == "PrivatniCasoviTeachers"
                                        select g;

            return requests.ToList();
        }

        public bool SecretaryDeclineClass(string classId)
        {
            try
            {
                PrivateClass  privateClass = (PrivateClass)new TableHelper(CLASSES.CLASS.ToString()).GetOne(classId);
                privateClass.ClassStatus = CLASS_STATUS.DECLINED.ToString();
                

                if (privateClass.ClassStatus == CLASS_STATUS.ACCEPTED.ToString())
                {
                    User teacher = GetClassTeacher(classId);
                    IQueryable<TeacherClass> requests = from g in table.CreateQuery<TeacherClass>()
                                                        where g.PartitionKey == _class.ToString() && g.TeachertId == int.Parse(teacher.RowKey) && g.ClassId == int.Parse(classId)
                                                        select g;

                    Delete(requests.ToList()[0]);

                    MailHandler.SendMail(teacher.PrefferEmail,"Secreatry DECLINED class",$"Secretary has DECLINED class {privateClass.Lesson} at {privateClass.Date}.");
                }
                TableHelper sc = new TableHelper(CLASSES.STUDENTCLASS.ToString());

                List<User> students = sc.GetClassStudents(classId);

                IQueryable<StudentClass> studentsreq = from g in sc.table.CreateQuery<StudentClass>()
                                                       where g.PartitionKey == sc._class.ToString() && g.ClassId == int.Parse(classId)
                                                       select g;

                studentsreq.ToList().ForEach(x => {

                    sc.Delete(x);
                });

                students.ForEach(x =>
                {
                    MailHandler.SendMail(x.PrefferEmail, "Secreatry DECLINED class", $"Secretary has DECLINED class {privateClass.Lesson} at {privateClass.Date}.");

                });
                new TableHelper(CLASSES.CLASS.ToString()).AddOrReplace(privateClass);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int StudentJoinClass(string userId, string classId)
        {
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                if (privateClass.NumberOfStudents >= 4)
                    return -1;
                else
                    privateClass.NumberOfStudents++;

                TableHelper tStudentClass = new TableHelper(CLASSES.STUDENTCLASS.ToString());

                var request = from g in tStudentClass.table.CreateQuery<StudentClass>()
                              where g.PartitionKey == tStudentClass._class.ToString() && g.StudentId == int.Parse(userId) && g.ClassId == int.Parse(classId)
                              select g;

                if (request.ToList().Count != 0)
                {
                    return -2;
                }

                StudentClass studentClass = new StudentClass(int.Parse(userId), int.Parse(classId), -1, false);
                AddOrReplace(privateClass);
                tStudentClass.AddOrReplace(studentClass);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int GetSubjectId(string name)
        {
            IQueryable<Subject> requests = from g in table.CreateQuery<Subject>()
                                           where g.PartitionKey == _class.ToString() && g.Name == name
                                           select g;

            return int.Parse(requests.ToList()[0].RowKey);
        }

        public int GetPricelistId(int subjectId)
        {
            IQueryable<Pricelist> requests = from g in table.CreateQuery<Pricelist>()
                                             where g.PartitionKey == _class.ToString() && g.SubjectId == subjectId
                                             select g;

            return requests.ToList()[0].Price;
        }

        public bool StudentAddClass(AddPrivateClassBindingModel model, PrivateClass privateClass, string userId)
        {
            try
            {

                privateClass.SubjectId = new TableHelper(CLASSES.SUBJECT.ToString()).GetSubjectId(model.Subject);
                privateClass.Price = new TableHelper(CLASSES.PRICELIST.ToString()).GetPricelistId(privateClass.SubjectId);

                AddOrReplace(privateClass);

                StudentClass studentClass = new StudentClass(int.Parse(userId), int.Parse(privateClass.RowKey), -1, false);
                new TableHelper(CLASSES.STUDENTCLASS.ToString()).AddOrReplace(studentClass);


                return true;
            }
            catch
            {
                return false;
            }


        }

        public bool SecretaryAddClass(AddPrivateClassBindingModel model, PrivateClass privateClass)
        {
            try
            {

                privateClass.SubjectId = new TableHelper(CLASSES.SUBJECT.ToString()).GetSubjectId(model.Subject);
                privateClass.Price = new TableHelper(CLASSES.PRICELIST.ToString()).GetPricelistId(privateClass.SubjectId);

                AddOrReplace(privateClass);



                return true;
            }
            catch
            {
                return false;
            }


        }

        public List<string> GetAllSubjects()
        {
            var requests = from g in table.CreateQuery<Subject>()
                           where g.PartitionKey == _class.ToString()
                           select g.Name;

            return requests.ToList();
        }

        public void CheckClassStatus()
        {
            IQueryable<PrivateClass> requests = from g in table.CreateQuery<PrivateClass>()
                                                where g.PartitionKey == _class.ToString()
                                                select g;

            foreach (PrivateClass item in requests.ToList())
            {
                if (item.ClassStatus != CLASS_STATUS.ACCEPTED.ToString())
                {
                    if (DateTime.Now > (item.Date - new TimeSpan(24, 0, 0)))
                    {
                        TableHelper ts = new TableHelper(CLASSES.TEACHERSUBJECT.ToString());

                        List<string> teachersEmail = ts.GetSubjectTeachersEmail(item.SubjectId.ToString());

                        foreach (string email in teachersEmail)
                        {
                            MailHandler.SendMail(email, "Class expires in 12h!", $"Class {item.Lesson} at {item.Date} expires in 12h, accept it if you are available.");
                        }
                    }
                    else if (DateTime.Now > (item.Date - new TimeSpan(12, 0, 0)))
                    {
                        item.ClassStatus = CLASS_STATUS.DECLINED.ToString();
                        AddOrReplace(item);
                        //posalji svim studentima mail

                        TableHelper sc = new TableHelper(CLASSES.STUDENTCLASS.ToString());

                        List<User> students = sc.GetClassStudents(item.RowKey);

                        foreach (User user in students)
                        {
                            MailHandler.SendMail(user.PrefferEmail, "Class is DECLINED!", $"Dear {user.Username}, Class {item.Lesson} at {item.Date} is declined because we dont have teacher for that time, please choose other time,we are truly sorry.");
                        }

                    }
                }
            }
        }

        public List<string> GetNotTeacherSubjectsAsync(string userId)
        {
            TableHelper ts = new TableHelper(CLASSES.TEACHERSUBJECT.ToString());
            TableHelper s = new TableHelper(CLASSES.SUBJECT.ToString());
            IQueryable<int> requests = from g in ts.table.CreateQuery<TeacherSubject>()
                                                where g.PartitionKey == ts._class.ToString() && g.TeachertId == int.Parse(userId)
                                                  select g.SubjectId;

            List<string> teaherSubjects = new List<string>();
            List<string> retVal = new List<string>();
            List<string> allSubjects = s.GetAllSubjects();

            requests.ToList().ForEach(x =>
            {
                teaherSubjects.Add(((Subject)s.GetOne(x.ToString())).Name);
            });

            foreach (string item in allSubjects)
            {
                if (!teaherSubjects.Contains(item))
                    retVal.Add(item);
            }

            return retVal;
        }

        #endregion
    }
}