using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangeAPI.Model
{
    public class User
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
    }

    public class Value
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}