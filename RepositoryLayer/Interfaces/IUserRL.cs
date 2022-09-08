using ModelLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public string AddUser(UserPostModel user);
        public string Login(UserLoginModel loginUser);
        public bool ForgetPasswordUser(string email);
        public bool ResetPassoword(string email, UserResetPasswordModel PasswordModel);
    }
}
