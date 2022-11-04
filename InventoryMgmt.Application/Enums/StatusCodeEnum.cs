using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Enums
{
    public enum StatusCodeEnum
    {
        Success=200,
        Created=201,
        NotFound=404,
        BadRequest=400,
        InternalServerError=500
    }
}
