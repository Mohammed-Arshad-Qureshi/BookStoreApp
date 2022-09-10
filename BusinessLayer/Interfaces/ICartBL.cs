using ModelLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddBookTOCart(int UserId, CartPostModel postModel);

    }
}
