using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        [Display(Name = "Логін")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}