﻿using GreenCart.Models;

namespace GreenCart.Repository
{
    public interface ICartRepository
    {
        Cart GetByUserId(int userId);
        void AddItem(int userId, int productId);
        void RemoveItem(int cartItemId);
        void ClearCart(int userId);
    }
}
