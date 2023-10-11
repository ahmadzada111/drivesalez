using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email cannot be blank!")]
        [EmailAddress(ErrorMessage = "Email address should be in a proper format!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be blank!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
