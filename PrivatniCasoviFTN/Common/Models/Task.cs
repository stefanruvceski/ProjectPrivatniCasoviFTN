using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Task
    {
        public int TaskID { get; set; }

        [Required]
        public string TaskText { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        public string CreatorName { get; set; }

        [Required]
        public virtual ICollection<AadObject> SharedWith { get; set; }
    }
}
