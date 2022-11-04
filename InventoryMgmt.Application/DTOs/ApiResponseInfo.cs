using InventoryMgmt.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs
{
    public class ApiResponseInfo
    {
        public ApiResponse Info { get; set; } = new ApiResponse();
        public object Data { get; set; }
    }


    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public StatusCodeEnum Code { get; set; }
    }
}
