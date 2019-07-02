using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class StudentClass : CustomEntity
    {
        static int cnt = 0;

        public StudentClass() { }

        public StudentClass(int studentId, int classId, int commentId, bool attended)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.STUDENTCLASS.ToString()).GetCount();
            }
            StudentId = studentId;
            ClassId = classId;
            CommentId = commentId;
            Attended = attended;
            PartitionKey = CLASSES.STUDENTCLASS.ToString();
            RowKey = (cnt++).ToString();
        }

        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int CommentId { get; set; }
        public bool Attended { get; set; }
    }
}
