using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.UserModel
{
    public class UserResetPasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
