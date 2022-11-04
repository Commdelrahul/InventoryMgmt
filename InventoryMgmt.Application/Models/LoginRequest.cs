using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Models
{
    public class LoginRequest
    {

        [Required]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }
        [Required]
     
        public string Password { get; set; }

    }
}
