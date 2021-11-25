using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public bool isActive { get; set; }
        public string Details { get; set; }
        public virtual List<ProductPropertyDTO> Properties { get; set; } = new List<ProductPropertyDTO>();

        public virtual List<ProductImageDTO> Images { get; set; } = new List<ProductImageDTO>();
    }
}
