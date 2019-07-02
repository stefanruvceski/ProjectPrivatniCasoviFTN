using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataBase_Models
{
    public class Comment : CustomEntity
    {
        static int cnt = 0;

        public Comment() { }

        public Comment(string description, int rating)
        {
            if (cnt == 0)
            {
                cnt = new TableHelper(CLASSES.COMMENT.ToString()).GetCount();
            }
            Description = description;
            Rating = rating;
            PartitionKey = CLASSES.COMMENT.ToString();
            RowKey = (cnt++).ToString();
        }

        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
