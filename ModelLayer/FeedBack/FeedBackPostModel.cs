using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace ModelLayer.FeedBack
{
    public class FeedBackPostModel
    {
        [Required]
        [DefaultValue("0")]
        public int BookId { get; set; }

        [Required]
        [DefaultValue("0.0")]
        public decimal Rating { get; set; }

        [Required]
        [DefaultValue("")]
        public string Comment { get; set; }
    }
}
