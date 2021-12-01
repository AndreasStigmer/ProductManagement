using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public interface IProductRepository
    {

        public Task<ProductDTO> GetProduct(int productid);
        public Task<ICollection<ProductDTO>> GetAllProducts();
        public Task<ProductDTO> CreateProduct(ProductDTO item);
        public Task<ProductDTO> UpdateProduct(ProductDTO item);
        public Task<int>DeleteProduct(int id);
        public Task<ProductDTO> IsProductUniqe(string name);
    }
}
