using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace ModelLayer.CartModel
{
    public class CartUpdateModel
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [DefaultValue("1")]
        [Range(1, 1000, ErrorMessage = "Book Quantity Exceeded it limit!!")]
        public int BookQuantity { get; set; }
    }
}
