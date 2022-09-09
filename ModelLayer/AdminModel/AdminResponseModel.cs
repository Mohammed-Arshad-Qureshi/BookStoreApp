using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.AdminModel
{
    public class AdminResponseModel
    {
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public string AdminEmail { get; set; }

        public string AdminPassword { get; set; }

        public string AdminAddress { get; set; }

        public long AdminMobile { get; set; }
    }
}
