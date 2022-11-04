using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMgmt.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        public AccountController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public IActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _loginRepository.ValidateUser(request);
                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    HttpContext.Session.SetString("Email", request.Email);
                }

                return Json(response, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {

                return Json(ModelState);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Logout()
        {
           
            HttpContext.Session.Clear();

            return Json("Logout successfully");
        }
    }
}
