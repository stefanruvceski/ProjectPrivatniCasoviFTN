using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class Pricelist : CustomEntity
    {
        static int cnt = 0;

        public Pricelist() { }
        
        public Pricelist(int price, int subjectId, int firmId)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.PRICELIST.ToString()).GetCount();
            }
            Price = price;
            SubjectId = subjectId;
            FirmId = firmId;
            PartitionKey = CLASSES.PRICELIST.ToString();
            RowKey = (cnt++).ToString();
        }

        public int Price { get; set; }
        public int SubjectId { get; set; }
        public int FirmId { get; set; }
    }
}
