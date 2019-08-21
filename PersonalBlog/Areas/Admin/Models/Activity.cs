using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Admin.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Date { get; set; }
        public string  ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public string Thumbnail { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        public string UplodedAt { get; set; }
        public string UplodedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string updatedBy { get; set; }

    }
}