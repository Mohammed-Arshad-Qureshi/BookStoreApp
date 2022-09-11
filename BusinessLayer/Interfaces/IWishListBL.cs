using ModelLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public bool AddToWishList(int UserId, int BookId);
    }
}
