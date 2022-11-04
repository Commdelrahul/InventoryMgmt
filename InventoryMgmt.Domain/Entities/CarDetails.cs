using InventoryMgmt.Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class CarDetails:BaseEntity
    {
        [Required]
        public string CarName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public bool IsNew { get; set; }
    }
}
