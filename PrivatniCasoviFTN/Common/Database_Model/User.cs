using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class User : CustomEntity
    {
        static int cnt = 0;
        public User() { }

        public User(string username, string firstName, string lastName, string address, string phone, string email,string prefferEmail, int firmId, string degreeOfEducation,string type)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.USER.ToString()).GetCount();
            }

            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            Email = email;
            PrefferEmail =prefferEmail;
            FirmId = firmId;
            AttendedClasses = 0;
            DegreeOfEducation = degreeOfEducation;
            PartitionKey = CLASSES.USER.ToString();
            RowKey = (cnt++).ToString();
            Type = Type;

        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PrefferEmail { get; set; }
        public int FirmId { get; set; }
        public int AttendedClasses { get; set; }
        public string DegreeOfEducation { get; set; }
    }
}
