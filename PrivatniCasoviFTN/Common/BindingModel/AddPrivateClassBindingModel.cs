using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.BindingModel
{
    [DataContract]
    public class AddPrivateClassBindingModel
    {
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string Lesson { get; set; }
    }
}
