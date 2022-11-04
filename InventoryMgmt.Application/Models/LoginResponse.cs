using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string ErrorMessage { get; set; }

    }
}
