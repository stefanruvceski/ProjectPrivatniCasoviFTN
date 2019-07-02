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
        CloudTable table;
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
        #endregion
    }
}