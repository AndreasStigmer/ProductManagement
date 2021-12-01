using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> RemoveImageByName(string filename)
        {
            if(filename != null)
            {
                var image = await db.Images.FirstOrDefaultAsync(i => i.Url == filename);
                if(image!=null) { 
                    db.Images.Remove(image);
                    return await db.SaveChangesAsync();
                }else
                {
                    return -1;
                }
            }
            return -1;
        }

        public async Task<int> RemoveImageByProductId(int productid)
        {
            if (productid != 0)
            {
                var images = db.Images.Where(i => i.ProductId == productid);
                db.Images.RemoveRange(images);
                return await db.SaveChangesAsync();
            }
            return -1;
        }
    }
}
