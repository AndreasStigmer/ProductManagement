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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext db;
        private readonly IMapper mapper;
        
        
        public ProductRepository(ProductDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task<ProductDTO> CreateProduct(ProductDTO item)
        {
           if(item!=null && item.Id==0)
            {
                var product = mapper.Map<ProductDTO, Product>(item);
                var newentry=db.Products.Add(product);
                await db.SaveChangesAsync();
                return mapper.Map<Product, ProductDTO>(newentry.Entity);
            }else
            {
                return null;
            }
        }

        public async Task<int> DeleteProduct(int id)
        {
            if(id!=0)
            {
                var prod = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                db.Products.Remove(prod);
                return await db.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<ICollection<ProductDTO>> GetAllProducts()
        {
            return mapper.Map<ICollection<Product>, ICollection<ProductDTO>>(await db.Products.Include(p => p.Images).ToListAsync());
        }

        public async Task<ProductDTO> GetProduct(int productid)
        {
            var prod = await db.Products.Include(p=>p.Images).FirstOrDefaultAsync(p=>p.Id==productid);
            var dto = mapper.Map<Product, ProductDTO>(prod);
            return dto;

        }

        public Task<ProductDTO> IsProductUniqe(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO item)
        {
            Product p = db.Products.Include(p=>p.Images).FirstOrDefault(p => p.Id == item.Id);

            p.Name = item.Name;
            p.isActive = item.isActive;
            p.Price = item.Price;

            var obj = db.Entry<Product>(p).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return mapper.Map<Product,ProductDTO>(p);
        }
    }
}
