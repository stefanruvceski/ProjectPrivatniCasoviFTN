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
            if (_class.Equals(CLASSES.USER))
            {
                TableOperation add = TableOperation.InsertOrReplace((User)entity);
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

            return null;
        }

        public dynamic GetCustomEntities(string id, string group)
        {
            if (_class.Equals(CLASSES.CLASS))
            {
                if (group == "PrivatniCasoviStudents")
                {
                    TableHelper tsc = new TableHelper(CLASSES.STUDENTCLASS.ToString());
                    TableHelper ts = new TableHelper(CLASSES.SUBJECT.ToString());
                    TableHelper ttc = new TableHelper(CLASSES.TEACHERCLASS.ToString());
                    TableHelper tt = new TableHelper(CLASSES.USER.ToString());
                    var requests = from pc in table.CreateQuery<PrivateClass>().ToList()
                                   join sc in tsc.table.CreateQuery<StudentClass>().ToList() on pc.RowKey equals sc.ClassId.ToString()
                                   join s in ts.table.CreateQuery<Subject>().ToList() on pc.SubjectId.ToString() equals s.RowKey
                                   join tc in ttc.table.CreateQuery<TeacherClass>().ToList() on pc.RowKey equals tc.ClassId.ToString()
                                   join t in tt.table.CreateQuery<User>().ToList() on tc.TeachertId.ToString() equals t.RowKey
                                   where  sc.StudentId.ToString() == id
                                   select new PrivateClassBindingModel { Id = pc.RowKey,Subject = s.Name, Teacher = t.Username, Status = pc.ClassStatus.ToString(),Date = pc.Date.ToShortDateString(),Lesson = pc.Lesson,NumberOfStudents = pc.NumberOfStudents.ToString()};

                    return requests.ToList().GroupBy(customer => customer.Id).Select(g => g.First()).ToList();
                }
                else if(group == "PrivatniCasoviTeachers")
                {
                    var requests = from pc in table.CreateQuery<PrivateClass>()
                                   join tc in table.CreateQuery<TeacherClass>() on pc.RowKey equals tc.ClassId.ToString()
                                   join t in table.CreateQuery<User>() on tc.TeachertId.ToString() equals t.RowKey
                                   where tc.TeachertId.ToString() == id
                                   select new { Id = pc.RowKey, Teacher = t.Username, Status = pc.ClassStatus, Date = pc.Date, Lesson = pc.Lesson, NumberOfStudents = pc.NumberOfStudents };

                    return requests.ToList();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        #endregion
    }
}