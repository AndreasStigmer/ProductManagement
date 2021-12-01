using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public interface IProductImageRepository
    {

        public Task<int> AddProductImage(ProductImageDTO image);
        public Task<int> RemoveImageByName(string imageid);
        public Task<int> RemoveImageByProductId(int productid);
    }
}
