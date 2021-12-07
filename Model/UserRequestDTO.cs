using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Används för att registrera nya användare
    /// </summary>
    public class UserRequestDTO
    {

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
