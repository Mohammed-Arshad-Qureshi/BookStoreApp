using ModelLayer.AdminModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        public string AdminLogin(AdminLoginModel loginModel);

    }
}
