using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChefBot.Areas.Admin.Models
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Kullanıcı adınız boş!")]
        public string Username { get; set; }
        [DisplayName("Şifre")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Şifreniz boş!")]
        public string Password { get; set; }
    }
}