using ModelLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public bool AddToWishList(int UserId, int BookId);
        public List<WishListResponseModel> GetAllWishList(int UserId);
        public bool DeleteWishListItem(int UserId, int WishListId);

    }
}
