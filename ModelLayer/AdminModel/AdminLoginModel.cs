using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace ModelLayer.AdminModel
{
    public class AdminLoginModel
    {
        [Required]
        [DefaultValue("")]
        public string AdminEmail { get; set; }

        [Required]
        [DefaultValue("")]
        public string AdminPassword { get; set; }
    }
}
