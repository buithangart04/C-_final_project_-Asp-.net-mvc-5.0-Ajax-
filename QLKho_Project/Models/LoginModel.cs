using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKho_Project.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage =" Bạn chưa nhập tên đăng nhập")]
        public string username { get; set; }

        [Required(ErrorMessage = " Bạn chưa nhập tên mật khẩu")]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}