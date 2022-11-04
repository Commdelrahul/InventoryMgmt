using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Models
{
    public class CarDetailRequest
    {
        public int skip { get; set; }
        public int pageSize { get; set; }
        public string draw { get; set; }
        public string sortColumnDirection { get; set; }
        public string sortColumn { get; set; }
        public string searchValue { get; set; }
        

    }
}
