using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_WASM.Service
{
    public interface IProductService
    {

        public Task<List<ProductDTO>> GetProducts();
        public Task<ProductDTO> GetProduct(int prodid);
    }
}
