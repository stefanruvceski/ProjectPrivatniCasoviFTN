using Common.BindingModel;
using Common.BindingModels;
using Common.DataBase_Models;
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

        public bool AcceptClass(string userId, string classId)
        {
            if (_class.Equals(CLASSES.CLASS))
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                privateClass.ClassStatus = CLASS_STATUS.ACCEPTED.ToString();
                TableOperation replace = TableOperation.Replace(privateClass);
                table.Execute(replace);

                TeacherClass teacherClass = new TeacherClass(int.Parse(userId), int.Parse(classId), -1, false);
                new TableHelper(CLASSES.TEACHERCLASS.ToString()).AddOrReplace(teacherClass);
                return true;
            }
            return false;
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
                    for (int i = 0;i< retVal.Count;i++)
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
                    var requests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                   join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey
                                   where pc.ClassStatus == CLASS_STATUS.REQUESTED.ToString() || pc.ClassStatus == CLASS_STATUS.ACCEPTED.ToString()
                                   select new PrivateClassBindingModel { Id = pc.RowKey, Subject = s.Name, Teacher = "", Status = pc.ClassStatus.ToString(), StartDate = pc.Date.ToString(), Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents.ToString() };

                    return requests.ToList().GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        private StudentClass GetStudentClass(string userId, string classId)
        {
            IQueryable<StudentClass> requests = from g in table.CreateQuery<StudentClass>()
                                                where g.PartitionKey == _class.ToString() && g.StudentId == int.Parse(userId) && g.ClassId == int.Parse(classId)
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
        public string UserChangeDate(string userId,string classId,DateTime dateTime)
        {
            string retval = $"success_{dateTime}";
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                if(privateClass.NumberOfStudents != 1)
                {
                    return $"There are more students in this class,you cant move it_{privateClass.Date}";
                }
                if(new TableHelper(CLASSES.STUDENTCLASS.ToString()).GetStudentClassCount(userId, classId) != 1)
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
        public bool UserDeclineClass(string userId,string classId)
        {
            try
            {
                PrivateClass privateClass = (PrivateClass)GetOne(classId);
                privateClass.NumberOfStudents--;
                if (privateClass.NumberOfStudents == 0)
                    privateClass.ClassStatus = CLASS_STATUS.DECLINED.ToString();

                AddOrReplace(privateClass);
                StudentClass studentClass = new TableHelper(CLASSES.STUDENTCLASS.ToString()).GetStudentClass(userId, classId);
                new TableHelper(CLASSES.STUDENTCLASS.ToString()).Delete(studentClass);

                return true;
            }
            catch
            {
                return false;
            }



            
        }

        public int StudentJoinClass(string  userId, string classId)
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

        public bool AddClass(AddPrivateClassBindingModel model,PrivateClass privateClass, string userId)
        {
            try
            {
               
                privateClass.SubjectId = new TableHelper(CLASSES.SUBJECT.ToString()).GetSubjectId(model.Subject);
                privateClass.Price = new TableHelper(CLASSES.PRICELIST.ToString()).GetPricelistId(privateClass.SubjectId);

                AddOrReplace(privateClass);

                StudentClass studentClass = new StudentClass(int.Parse(userId),int.Parse(privateClass.RowKey),-1,false);
                new TableHelper(CLASSES.STUDENTCLASS.ToString()).AddOrReplace(studentClass);


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
        #endregion
    }
}