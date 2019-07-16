using Common.DataBase_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class TeacherClass : CustomEntity
    {
        static int cnt = 0;

        public TeacherClass() { }

        public TeacherClass(int teacherId, int classId, int commentId, bool attended)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.TEACHERCLASS.ToString()).GetCount();
            }
            TeachertId = teacherId;
            ClassId = classId;
            CommentId = commentId;
            Attended = attended;
            PartitionKey = CLASSES.TEACHERCLASS.ToString();
            RowKey = (cnt++).ToString();
        }

        public int TeachertId { get; set; }
        public int ClassId { get; set; }
        public int CommentId { get; set; }
        public bool Attended { get; set; }
    }
}
