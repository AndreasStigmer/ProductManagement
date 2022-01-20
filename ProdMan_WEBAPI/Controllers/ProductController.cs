using Business.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository prodRepo;

        public ProductController(IProductRepository prodRepo)
        {
           
            this.prodRepo = prodRepo;
        }

        [HttpGet]
       
        public async Task<IActionResult> GetProducts()
        {
         
            return Ok(await prodRepo.GetAllProducts());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(id<1)
            {
                return BadRequest("Id not valid");
            }

            var prod = await prodRepo.GetProduct(id);

            if(prod==null)
            {
                return NotFound("Product id could not be found");
            }

            return Ok(prod);
        }
    }
}
