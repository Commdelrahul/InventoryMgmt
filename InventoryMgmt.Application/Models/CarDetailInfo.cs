using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Models
{
   public class CarDetailInfo
    {
     
        public int Id { get; set; }
        public string CarName { get; set; }
        public double Price { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string IsNew { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

    }
}
