using InventoryMgmt.Application.DTOs;
using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Application.Models;
using InventoryMgmt.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LoginRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            try
            {


                response = await (from a in _dbContext.Login
                                 where a.Email.ToLower() == loginRequest.Email.ToLower() && a.Password.ToLower() == loginRequest.Password.ToLower() && a.IsActive
                                 select new LoginResponse
                                 {
                                     Email = a.Email,
                                     Id = a.Id

                                 }).FirstOrDefaultAsync();

                if (response == null)
                {
                      response = new LoginResponse();
                    response.ErrorMessage = "Email or Password is invalid";
                }
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
