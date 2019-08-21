using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Admin.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase Images { get; set; }
        public string UplodedAt { get; set; }
        public string UplodedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string updatedBy { get; set; }

    }
}