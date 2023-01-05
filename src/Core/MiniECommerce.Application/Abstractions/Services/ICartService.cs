using MiniECommerce.Application.DTOs.NCartItem;
using MiniECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Abstractions.Services
{
    public interface ICartService
    {
        public Task<List<CartItem>> GetCartItemsAsync();
        public Task AddCartItemAsync(CreateCartItemDto cartItemDto);
        public Task UpdateCartItemAsync(UpdateCartItemDto cartItemDto);
        public Task DeleteCartItemAsync(string cartItemId);
    }
}
