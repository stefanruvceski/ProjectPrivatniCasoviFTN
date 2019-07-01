using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class UserTokenCache
    {
        [Key]
        public int UserTokenCacheId { get; set; }

        public string webUserUniqueId { get; set; }

        public byte[] cacheBits { get; set; }

        public DateTime LastWrite { get; set; }
    }
}
