using Common.DataBase_Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database_Models
{
    public class TeacherSubject : CustomEntity
    {
        static int cnt = 0;

        public TeacherSubject() { }

        public TeacherSubject(int teacherId, int subjectId)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.TEACHERCLASS.ToString()).GetCount();
            }
            TeachertId = teacherId;
            SubjectId = subjectId;
            HeldClasses =0;
            PartitionKey = CLASSES.TEACHERSUBJECT.ToString();
            RowKey = (cnt++).ToString();
        }

        public int TeachertId { get; set; }
        public int SubjectId { get; set; }

        public int HeldClasses;
    }
}
