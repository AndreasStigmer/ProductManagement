using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdMan_WASM.Commerce
{
    public interface ICartService
    {
        public event Action OnChange;
        public Task<List<CartItem>> GetCartItems();
        public Task<List<ProductDTO>> GetCartProductDTOs();
        public Task AddToCart(CartItem item);
        public Task ChangeQty(CartItem item,int qty);
        public Task RemoveItem(CartItem item);

    }
}
