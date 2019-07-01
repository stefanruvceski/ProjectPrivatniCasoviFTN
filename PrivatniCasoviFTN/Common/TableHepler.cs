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
        KLASE klasa;
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
            Enum.TryParse(tableName, out klasa);
        }
        #endregion

        public void InitTable(List<CustomEntity> list)
        {
            TableBatchOperation tableOperations = new TableBatchOperation();

            if (klasa.Equals(KLASE.PREDMET))
            {
                

                List<Predmet> predmeti = new List<Predmet>();
                list.ForEach(x=>predmeti.Add((Predmet)x));

                predmeti.ForEach(x => tableOperations.InsertOrReplace(x));
            }
           
            

            table.ExecuteBatch(tableOperations);
        }

        #region Operacije nad tabelom
        public void AddOrReplace(CustomEntity entity)
        {
            
            if (klasa.Equals(KLASE.PREDMET))
            {
                TableOperation add = TableOperation.InsertOrReplace((Predmet)entity);
                table.Execute(add);
            }
        }

        public void Delete(CustomEntity entity)
        {

            if (klasa.Equals(KLASE.PREDMET))
            {
                TableOperation delete = TableOperation.Delete((Predmet)entity);
                table.Execute(delete);
            }
        }

        public int GetCount()
        {
            int retVal = 0;

            if (klasa.Equals(KLASE.PREDMET))
            {
                IQueryable<Predmet> requests = from g in table.CreateQuery<Predmet>()
                                         where g.PartitionKey == klasa.ToString()
                                         select g;

                retVal = requests.ToList().Count();
            }

            return retVal;
        }

        public CustomEntity GetOne(string id)
        {
            if (klasa.Equals(KLASE.PREDMET))
            {
                IQueryable<Predmet> requests = from g in table.CreateQuery<Predmet>()
                                            where g.PartitionKey == klasa.ToString() && g.RowKey == id
                                            select g;

                return requests.ToList()[0];
            }
            return null;
        }
        #endregion
    }
}
