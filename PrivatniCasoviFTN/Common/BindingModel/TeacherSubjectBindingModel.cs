using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.BindingModel
{
    [DataContract]
    public class TeacherSubjectBindingModel
    {
        [DataMember]
        public List<SubjectByType> Subjects  { get; set; }
    }
    [DataContract]
    public class SubjectByType
    {
        [DataMember]
        public List<SubjectTeach> Subjects { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class SubjectTeach
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
