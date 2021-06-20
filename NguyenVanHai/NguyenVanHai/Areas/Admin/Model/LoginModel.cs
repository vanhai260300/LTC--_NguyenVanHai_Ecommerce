using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NguyenVanHai.Areas.Admin.Model
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Account entry required")]
        public string UserName { set; get; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password entry required")]
        public string PassWord { set; get; }
    }
}