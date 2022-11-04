using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Models
{
    public class CarDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter car name")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special character can not be allowed")]
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
