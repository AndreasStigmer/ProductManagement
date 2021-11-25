using AutoMapper;
using DataAccess.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ProductDbContext db;
        private readonly IMapper mapper;

        public ProductImageRepository(ProductDbContext _db,IMapper _mapper )
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<int> AddProductImage(ProductImageDTO image)
        {
           if(image.ProductId!=0)
            {
                var mapped = mapper.Map<ProductImageDTO, ProductImage>(image);
                db.Images.Add(mapped);
                return await db.SaveChangesAsync();
            }else
            {
                return -1;
            }
        }

        public Task<int> RemoveImageById(int imageid)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImageByProductId(int productid)
        {
            throw new NotImplementedException();
        }
    }
}
