using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace ModelLayer.OrderModel
{
    public class OrderPostModel
    {

        [Required]
        [DefaultValue("0")]
        public int CartId { get; set; }

        [Required]
        [DefaultValue("0")]
        public int AddressId { get; set; }
    }
}
