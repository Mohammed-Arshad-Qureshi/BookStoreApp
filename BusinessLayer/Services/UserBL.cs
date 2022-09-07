using BusinessLayer.Interfaces;
using ModelLayer.UserModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
        }

        public string AddUser(UserPostModel user)
        {
            try
            {
                return this._userRL.AddUser(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
