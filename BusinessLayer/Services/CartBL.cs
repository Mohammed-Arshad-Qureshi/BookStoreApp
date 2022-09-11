using BusinessLayer.Interfaces;
using ModelLayer.CartModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL _cartRL;

        public CartBL(ICartRL cartRL)
        {
            _cartRL = cartRL;
        }

        public bool AddBookTOCart(int UserId, CartPostModel postModel)
        {
            try
            {
                return _cartRL.AddBookTOCart(UserId, postModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CartResponseModel> GetAllBooksInCart(int UserId)
        {
            try
            {
                return _cartRL.GetAllBooksInCart(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CartResponseModel GetAllBooksInCartByCartId(int UserId, int CartId)
        {
            try
            {
                return _cartRL.GetAllBooksInCartByCartId(UserId,CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCartItem(int UserId, CartUpdateModel cartUpdateModel)
        {
            try
            {
                return _cartRL.UpdateCartItem(UserId, cartUpdateModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteCartbyCartId(int UserId, int CartId)
        {
            try
            {
                return _cartRL.DeleteCartbyCartId(UserId, CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}