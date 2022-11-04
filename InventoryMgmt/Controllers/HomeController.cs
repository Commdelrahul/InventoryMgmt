using InventoryMgmt.Application.Interfaces.IRepositories;
using InventoryMgmt.Application.Models;
using InventoryMgmt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMgmt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInventoryRepository _inventoryRepository;

        public HomeController(ILogger<HomeController> logger, IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {


                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetCars(CarDetailRequest request)
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            request.draw = draw;
            request.sortColumn = sortColumn;
            request.searchValue = searchValue;
            request.sortColumnDirection = sortColumnDirection;
            request.sortColumn = searchValue;
            request.pageSize = pageSize;
            request.skip = skip;

            var response = await _inventoryRepository.GetCarDetails(request);


            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddCarDetail([FromBody] CarDetail request)
        {
            var email = HttpContext.Session.GetString("Email");
            if (email != "")
            {
                var result = await _inventoryRepository.AddCarDetail(request);
                if (result > 0)
                {
                    var response = new
                    {
                        IsSuccess = true,
                        Message = "Data added successfully"
                    };
                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        IsSuccess = false,
                        Message = "Data not added, try again"
                    };
                    return Json(response);
                }
            }
            else
            {
                var response = new
                {
                    IsSuccess = false,
                    Message = "User not logged"
                };
                return Json(response);
            }
        }


        [HttpPut]
        public async Task<JsonResult> UpdateCarDetail([FromBody] CarDetail request)
        {
            var email = HttpContext.Session.GetString("Email");
            if (email != "")
            {
                var result = await _inventoryRepository.UpdateCarDetail(request);
                if (result > 0)
                {
                    var response = new
                    {
                        IsSuccess = true,
                        Message = "Data updated successfully"
                    };
                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        IsSuccess = false,
                        Message = "Data not updated, try again"
                    };
                    return Json(response);
                }
            }
            else
            {
                var response = new
                {
                    IsSuccess = false,
                    Message = "User not logged"
                };
                return Json(response);
            }
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCarDetail(int id)
        {
            var email = HttpContext.Session.GetString("Email");
            if (email != "")
            {
                var result = await _inventoryRepository.DeleteDetail(id);
                if (result > 0)
                {
                    var response = new
                    {
                        IsSuccess = true,
                        Message = "Data deleted successfully"
                    };
                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        IsSuccess = false,
                        Message = "Data not deleted, try again"
                    };
                    return Json(response);
                }
            }
            else
            {
                var response = new
                {
                    IsSuccess = false,
                    Message = "User not logged"
                };
                return Json(response);
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetCarDetailById(int id)
        {
            var email = HttpContext.Session.GetString("Email");
            if (email != "")
            {
                var data = await _inventoryRepository.GetCarDetailById(id);
                if (data != null)
                {
                    var response = new
                    {
                        IsSuccess = true,
                        Data = data
                    };
                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        IsSuccess = false,
                   
                    };
                    return Json(response);
                }
            }
            else
            {
                var response = new
                {
                    IsSuccess = false,
                    Message = "User not logged"
                };
                return Json(response);
            }
        }



        public IActionResult AddCarDetail()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {


                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
