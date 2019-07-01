using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class Predmet : CustomEntity
    {
        static int cnt = 0;
        public Predmet() { }
        public Predmet(string naziv)
        {
            if(cnt == 0)
            {
                cnt = new TableHelper(KLASE.PREDMET.ToString()).GetCount();
            }
            Naziv = naziv;
            PartitionKey = KLASE.PREDMET.ToString();
            RowKey = (cnt++).ToString();
           
        }
        public string Naziv { get; set; }
        
    }
}