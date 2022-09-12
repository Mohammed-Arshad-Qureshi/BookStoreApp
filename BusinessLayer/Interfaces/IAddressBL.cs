using ModelLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public bool AddAddress(int UserId, AddressPostModel postModel);
        public List<AddressResponseModel> GetAllAddresses(int UserId);
        public bool UpdateAddressbyId(int UserId, AddressUpdateModel postModel);
        public bool DeleteAddressByAddressId(int AddressId, int UserId);
        public AddressResponseModel GetAllAddressById(int UserId, int AddressId);

    }
}
