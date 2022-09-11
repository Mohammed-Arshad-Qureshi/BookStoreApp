using BusinessLayer.Interfaces;
using ModelLayer.WishListModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL:IWishListBL
    {
        private readonly IWishListRL _wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            _wishListRL = wishListRL;
        }

        public bool AddToWishList(int UserId, int BookId)
        {
            try
            {
                return _wishListRL.AddToWishList(UserId,BookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<WishListResponseModel> GetAllWishList(int UserId)
        {
            try
            {
                return _wishListRL.GetAllWishList(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteWishListItem(int UserId, int WishListId)
        {
            try
            {
                return _wishListRL.DeleteWishListItem(UserId,WishListId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
