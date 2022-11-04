using InventoryMgmt.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs
{
    public class CarDetailDto
    {
        public List<CarDetailInfo> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public string draw { get; set; }
        
    }
     
}
