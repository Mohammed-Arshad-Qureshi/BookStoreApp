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
        public string Login(UserLoginModel loginUser)
        {
            try
            {
                return this._userRL.Login(loginUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool ForgetPasswordUser(string email)
        {
            try
            {
                return this._userRL.ForgetPasswordUser(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool ResetPassoword(string email, UserResetPasswordModel PasswordModel)
        {
            try
            {
                return this._userRL.ResetPassoword(email,PasswordModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
