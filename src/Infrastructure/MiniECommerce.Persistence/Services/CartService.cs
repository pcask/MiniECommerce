using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.DTOs.NCartItem;
using MiniECommerce.Application.Exceptions;
using MiniECommerce.Application.Repositories.NCart;
using MiniECommerce.Application.Repositories.NCartItem;
using MiniECommerce.Application.Repositories.NOrder;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartReadRepository _cartReadRepository;
        private readonly ICartWriteRepository _cartWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly ICartItemReadRepository _cartItemReadRepository;
        private readonly ICartItemWriteRepository _cartItemWriteRepository;
        private readonly UserManager<AppUser> _userManager;

        public CartService(IHttpContextAccessor httpContextAccessor, ICartWriteRepository cartWriteRepository, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, ICartItemReadRepository cartItemReadRepository, ICartItemWriteRepository cartItemWriteRepository, ICartReadRepository cartReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartReadRepository = cartReadRepository;
            _cartWriteRepository = cartWriteRepository;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _cartItemReadRepository = cartItemReadRepository;
            _cartItemWriteRepository = cartItemWriteRepository;
        }

        private async Task<Cart> GetCurrentCartAsync()
        {
            string userEmail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            if (String.IsNullOrEmpty(userEmail))
                throw new AuthenticationErrorException();

            AppUser? currentUser = _userManager.Users
                .Include(u => u.Carts)
                .FirstOrDefault(u => u.Email == userEmail);

            if (currentUser == null)
                throw new NotFoundUserException();

            // Cart ve Order tablosu arasında left join kurup, ardından order'ı olmayan (yani siparişi tamamlanmayan, aktif sepet'i) cart'ı arıyoruz.
            var cartOrder = from cart in currentUser.Carts
                            join ord in _orderReadRepository.Table
                            on cart.Id equals ord.CartId
                            into CartOrderGroup
                            from order in CartOrderGroup.DefaultIfEmpty()
                            select new { cart, order };

            Cart targetCart = null;

            if (cartOrder.Any(co => co.order == null))
                targetCart = cartOrder.FirstOrDefault(co => co.order == null).cart;
            else // Eğer order'ı olmayan bir cart yoksa yeni bir cart oluşturuyoruz;
            {
                targetCart = new Cart();
                currentUser.Carts.Add(targetCart);
                await _cartWriteRepository.SaveAsync();
            }

            return targetCart;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            Cart currentCart = await GetCurrentCartAsync();

            return await _cartItemReadRepository.Table
                 .Include(ci => ci.Product)
                 .Where(ci => ci.CartId == currentCart.Id)
                 .ToListAsync();
        }

        public async Task AddCartItemAsync(CreateCartItemDto cartItemDto)
        {
            Cart currentCart = await GetCurrentCartAsync();

            var cartItem = await _cartItemReadRepository.GetAsync(ci => ci.CartId == currentCart.Id && ci.ProductId.ToString() == cartItemDto.ProductId);

            if (cartItem != null)
                cartItem.Quantity++;
            else
                await _cartItemWriteRepository.AddAsync(new()
                {
                    CartId = currentCart.Id,
                    ProductId = Guid.Parse(cartItemDto.ProductId),
                    Quantity = cartItemDto.Quantity,
                });

            await _cartItemWriteRepository.SaveAsync();
        }

        public async Task DeleteCartItemAsync(string cartItemId)
        {
            CartItem cartItem = await _cartItemReadRepository.GetByIdAsync(cartItemId);

            if (cartItem != null)
            {
                _cartItemWriteRepository.Remove(cartItem);

                await _cartItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDto cartItemDto)
        {
            CartItem cartItem = await _cartItemReadRepository.GetByIdAsync(cartItemDto.CartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = cartItemDto.Quantity;

                await _cartItemWriteRepository.SaveAsync();
            }
        }
    }
}
