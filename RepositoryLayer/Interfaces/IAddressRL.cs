using ModelLayer.AddressModel;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public bool AddAddress(int UserId, AddressPostModel postModel);
        public List<AddressResponseModel> GetAllAddresses(int UserId);
        public bool UpdateAddressbyId(int UserId, AddressUpdateModel postModel);
        public bool DeleteAddressByAddressId(int AddressId, int UserId);
        public AddressResponseModel GetAllAddressById(int UserId,int AddressId);
    }
}
