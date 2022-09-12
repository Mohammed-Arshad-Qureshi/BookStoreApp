using BusinessLayer.Interfaces;
using ModelLayer.AddressModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL _addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            _addressRL = addressRL;
        }

        public bool AddAddress(int UserId, AddressPostModel postModel)
        {
            try
            {
                return _addressRL.AddAddress(UserId, postModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AddressResponseModel> GetAllAddresses(int UserId)
        {
            try
            {
                return _addressRL.GetAllAddresses(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateAddressbyId(int UserId, AddressUpdateModel postModel)
        {
            try
            {
                return _addressRL.UpdateAddressbyId(UserId,postModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool DeleteAddressByAddressId(int AddressId, int UserId)
        {
            try
            {
                return _addressRL.DeleteAddressByAddressId(UserId, UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AddressResponseModel GetAllAddressById(int UserId, int AddressId)
        {
            try
            {
                return _addressRL.GetAllAddressById(UserId,AddressId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
