using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class Firm : CustomEntity
    {
        static int cnt = 0;
        public Firm() { }

        public Firm( string name, string phone, string address, string email, string city)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.FIRM.ToString()).GetCount();
            }
            Name = name;
            Phone = phone;
            Address = address;
            Email = email;
            City = city;
            PartitionKey = CLASSES.FIRM.ToString();
            RowKey = (cnt++).ToString();
        }
        
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}
