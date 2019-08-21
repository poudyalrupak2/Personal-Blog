using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Admin.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage ="Plese enter title")]
        public string Title { get; set; }
        public string shortDetail { get; set; }
        public string LongDescription { get; set; }
        [Required(ErrorMessage ="please enter publishing date")]
        public DateTime PublishingDate { get; set; }
        public string UplodedBy { get; set; }
        public DateTime UplodedAt { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFile Images { get; set; }
       
        public string UpdatedAt { get; set; }
        public string updatedBy { get; set; }


    }
}