using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.BindingModel
{
    [DataContract]
    public class SubjectBindingModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public List<SubjectTeacherBindingModel> Teachers { get; set; }
    }
    [DataContract]
    public class SubjectTeacherBindingModel
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Image { get; set; }
    }
}
