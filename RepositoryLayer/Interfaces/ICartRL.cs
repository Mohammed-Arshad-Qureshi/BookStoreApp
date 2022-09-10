using ModelLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public bool AddBookTOCart(int UserId, CartPostModel postModel);
        public List<CartResponseModel> GetAllBooksInCart(int UserId);
    }
}
