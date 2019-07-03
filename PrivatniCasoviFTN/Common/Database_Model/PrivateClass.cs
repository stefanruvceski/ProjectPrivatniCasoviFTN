using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class PrivateClass : CustomEntity
    {
        static int cnt = 0;

        public PrivateClass() { }

        public PrivateClass(string lesson, int firmId, int price, DateTime date, int subjectId, int numberOfStudents)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.CLASS.ToString()).GetCount();
            }
            Lesson = lesson;
            FirmId = firmId;
            Price = price;
            Date = date;
            SubjectId = subjectId;
            NumberOfStudents = numberOfStudents;
            PartitionKey = CLASSES.CLASS.ToString();
            RowKey = (cnt++).ToString();
            ClassStatus = CLASS_STATUS.REQUESTED.ToString();
        }

        public string ClassStatus { get; set; }
        public string Lesson { get; set; }
        public int FirmId { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
