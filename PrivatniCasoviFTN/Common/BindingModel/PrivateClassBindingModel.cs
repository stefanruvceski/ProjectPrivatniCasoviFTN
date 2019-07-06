using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.BindingModels
{
    [DataContract]
    public class PrivateClassBindingModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Teacher { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]

        public string Lesson { get; set; }
        [DataMember]

        public string NumberOfStudents { get; set; }
        [DataMember]
        public string IsMine { get; set; }
    }
}
