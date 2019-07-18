using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.BindingModels
{
    [DataContract]
    public class EditUserInfoBindingModel
    {
        public EditUserInfoBindingModel() { }

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string PrefferEmail { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Degree { get; set; }

        [DataMember]
        public string Image { get; set; }

    }
}
