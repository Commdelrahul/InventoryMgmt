using InventoryMgmt.Application.DTOs;
using InventoryMgmt.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Interfaces.IRepositories
{
   public interface ILoginRepository
    {
        public Task<LoginResponse> ValidateUser(LoginRequest loginRequest); 

    }
}
