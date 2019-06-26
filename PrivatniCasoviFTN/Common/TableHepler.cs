using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        #endregion

        //private void InitTable()
        //{
        //    TableBatchOperation tableOperations = new TableBatchOperation();

        //    Film a1 = new Film("123",10);
        //    Film a2 = new Film("456", 10);
        //    Film a3 = new Film("789", 10);
        //    Film a4 = new Film("000", 10);

        //    tableOperations.InsertOrReplace(a1);
        //    tableOperations.InsertOrReplace(a2);
        //    tableOperations.InsertOrReplace(a3);
        //    tableOperations.InsertOrReplace(a4);

        //    table.ExecuteBatch(tableOperations);
        //}

        //#region Operacije nad tabelom
        ////Operacije nad tabelom
        ////Find: Film -> Replace: Naziv klase koja se koristi
        //public void AddOrReplaceFilm(Film obj)
        //{
        //    TableOperation add = TableOperation.InsertOrReplace(obj);
        //    table.Execute(add);

        //}

        //public void DeleteFilm(Film obj)
        //{
        //    TableOperation delete = TableOperation.Delete(obj);
        //    table.Execute(delete);
        //}

        //public List<Film> GetAllFilms()
        //{
        //    IQueryable<Film> requests = from g in table.CreateQuery<Film>()
        //                                where g.PartitionKey == "Film"
        //                                select g;
        //    return requests.ToList();
        //}

        //public Film GetOneFilm(string id)
        //{
        //    IQueryable<Film> requests = from g in table.CreateQuery<Film>()
        //                                where g.PartitionKey == "Film" && g.RowKey == id
        //                                select g;

        //    return requests.ToList()[0];
        //}

        //public List<Film> GetAllFilmByName(string name)
        //{
        //    IQueryable<Film> requests = from g in table.CreateQuery<Film>()
        //                                where g.PartitionKey == "Film" && g.Naziv == name
        //                                select g;

        //    return requests.ToList();
        //}
        //#endregion
    }
}
