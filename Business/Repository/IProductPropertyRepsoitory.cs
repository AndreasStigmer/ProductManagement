using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public interface IProductPropertyRepository
    {

        public Task<IEnumerable<ProductPropertyDTO>> GetPropertyByProductId(int productid);
        public Task<ProductPropertyDTO> DeletePropertiesByProductId(int productid);
        public Task<ProductPropertyDTO> DeletePropertyByPropertyId(int propertyid);
        public Task<ProductDTO> CreateProductProperty(ProductPropertyDTO property);
    }
}
