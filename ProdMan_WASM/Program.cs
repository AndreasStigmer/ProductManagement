
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProdMan_WASM.Commerce;
using ProdMan_WASM.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace ProdMan_WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            await Task.Delay(2000); //For debugstartup
            builder.RootComponents.Add<App>("#app");
            var section = builder.Configuration.GetSection("ApiBaseUri");
            //await Task.Delay(5000);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(section.Value) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            
            await builder.Build().RunAsync();
        }
    }
}
