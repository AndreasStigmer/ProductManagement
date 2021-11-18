﻿using AutoMapper;
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

        public Task<int> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await db.Products.ToListAsync());
        }

        public async Task<ProductDTO> GetProduct(int productid)
        {
            var prod = await db.Products.FirstOrDefaultAsync(p=>p.Id==productid);
            var dto = mapper.Map<Product, ProductDTO>(prod);
            return dto;

        }

        public Task<ProductDTO> IsProductUniqe(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> UpdateProduct(ProductDTO item)
        {
            throw new NotImplementedException();
        }
    }
}