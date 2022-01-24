using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Model;
using ProdMan_WASM.Commerce;
using ProdMan_WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdMan_WASM.Helpers;

namespace ProdMan_WASM.Service
{
    public class CartService :ICartService
    {
        private readonly IMapper mapper;
        private readonly ILocalStorageService lstorage;
        private readonly IProductService prodservice;
        private readonly IJSRuntime runtime;
        private const string CartName = "SHOPCART";
        public CartService(IMapper mapper, ILocalStorageService lstorage, IProductService prodservice,IJSRuntime runtime)
        {
            this.mapper = mapper;
            this.lstorage = lstorage;
            this.prodservice = prodservice;
            this.runtime = runtime;
        }

        private List<CartItem> CartItems { get; set; }

        private async Task SaveCart(List<CartItem> items)
        {
            await lstorage.SetItemAsync<List<CartItem>>(CartName, items);
        }

        private async Task<List<CartItem>> LoadCart()
        {
            var Cart = await lstorage.GetItemAsync<List<CartItem>>(CartName);
            if(Cart==null)
            {
                return new List<CartItem>();
            }else
            {
                return Cart;
            }

        }

        
        
        public event Action OnChange;

        public async Task AddToCart(CartItem item)
        {
            var Cart = await LoadCart();
            var found = Cart.Find(i => i.Id == item.Id);
            if(found==null)
            {
                item.Quantity = 1;
                Cart.Add(item);
            }
            else
            {
                found.Quantity++;
            }
            await SaveCart(Cart);
            await runtime.SuccessToastr("Product added to cart");
            this.OnChange.Invoke();
        }

        public async Task ChangeQty(CartItem item, int qty)
        {
            var Cart = await lstorage.GetItemAsync<List<CartItem>>(CartName);
            var citem = Cart.Find(i => i.Id == item.Id);
            if(qty<1)
            {
                await RemoveItem(item);
            }else
            {
                citem.Quantity = qty;
                await SaveCart(Cart);
                await runtime.SuccessToastr("Number of items changed!");
                OnChange.Invoke();

            }

        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var Cart = await LoadCart();
            List<CartItem> newList = new List<CartItem>();
            foreach(var item in Cart)
            {
                newList.Add(item);
            }
            return newList;

        }

        public Task<List<ProductDTO>> GetCartProductDTOs()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveItem(CartItem item)
        {
            var Cart = await LoadCart();
            var found=Cart.Find(i=>i.Id==item.Id);
            Cart.Remove(found);
            await SaveCart(Cart);
            await runtime.SuccessToastr("Item was removed");
            
            OnChange.Invoke();
        }
    }
}
