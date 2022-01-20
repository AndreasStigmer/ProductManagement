using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProdMan_WASM.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;

        public ProductService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ProductDTO> GetProduct(int prodid)
        {
            var response = await client.GetAsync($"api/Product/{prodid}");
            var stringdata = await response.Content.ReadAsStringAsync();
            var prod = JsonConvert.DeserializeObject<ProductDTO>(stringdata);
            return prod;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var response = await client.GetAsync("api/Product");
            var stringdata = await response.Content.ReadAsStringAsync();
            var prod = JsonConvert.DeserializeObject<List<ProductDTO>>(stringdata);
            return prod;
        }
    }
}
