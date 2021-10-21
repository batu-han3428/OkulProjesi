using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mail alanı boş geçilemez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }
    }
}
