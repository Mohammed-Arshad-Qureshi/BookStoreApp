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
        public CartResponseModel GetAllBooksInCartByCartId(int UserId,int CartId);
        public bool UpdateCartItem(int UserId, CartUpdateModel cartUpdateModel);
    }
}
