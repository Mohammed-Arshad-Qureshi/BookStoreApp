using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.AddressModel
{
    public class AddressResponseModel
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public long MobileNo { get; set; }

        public int AddressType { get; set; }

        public string FullAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
