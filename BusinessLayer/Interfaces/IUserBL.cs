using ModelLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public string AddUser(UserPostModel user);

    }
}
