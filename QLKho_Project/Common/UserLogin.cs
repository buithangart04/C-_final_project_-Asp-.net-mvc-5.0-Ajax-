using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKho_Project.Common
{
    [Serializable]
    public class UserLogin
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}