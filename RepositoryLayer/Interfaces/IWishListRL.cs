using ModelLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public bool AddToWishList(int UserId, int BookId);
        public List<WishListResponseModel> GetAllWishList(int UserId);
        public bool DeleteWishListItem(int UserId, int WishListId);

    }
}
