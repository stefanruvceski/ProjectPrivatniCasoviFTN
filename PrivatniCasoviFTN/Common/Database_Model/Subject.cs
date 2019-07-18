using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.DataBase_Models
{
    public class Subject : CustomEntity
    {
        static int cnt = 0;
        public Subject() { }
        public Subject(string name,string type, string details)
        {
            if(cnt == 0)
            {
                cnt = new TableHelper(CLASSES.SUBJECT.ToString()).GetCount();
            }
            Name = name;
            Type = type;
            Details = details;
            PartitionKey = CLASSES.SUBJECT.ToString();
            RowKey = (cnt++).ToString();
           
        }
        public string Name { get; set; }

        public string Type { get; set; }
        public string Details { get; set; }
    }
}