using BusinessLayer.Interfaces;
using ModelLayer.AdminModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL _adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            _adminRL = adminRL;
        }

        public string AdminLogin(AdminLoginModel loginModel)
        {
            try
            {
                return this._adminRL.AdminLogin(loginModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
