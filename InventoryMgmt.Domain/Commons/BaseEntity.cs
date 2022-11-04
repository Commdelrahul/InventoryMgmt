using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Commons
{
    public abstract class   BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
    
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required, StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required, StringLength(100)]
        public string CreatedIPAddress { get; set; }
        [StringLength(100)]
        public string ModifiedIPAddress { get; set; }
    }
}
