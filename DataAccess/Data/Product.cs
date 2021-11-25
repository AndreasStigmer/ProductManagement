using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public bool isActive { get; set; }
        public string Details { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = System.DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = System.DateTime.Now;

        public virtual List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public virtual List<ProductProperty> Properties { get; set; } = new List<ProductProperty>();
    }
}
