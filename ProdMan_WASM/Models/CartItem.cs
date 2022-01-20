using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_WASM.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
