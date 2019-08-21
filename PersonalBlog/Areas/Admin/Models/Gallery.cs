using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Admin.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public HttpPostedFile Image { get; set; }
        public ICollection<Images> Images { get; set; }
    }

    public class Images
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Nooffile { get; set; }
        public string FilePath { get; set; }
        public virtual Gallery Gallery { get; set; }
        public string UplodedAt { get; set; }
        public string UplodedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string updatedBy { get; set; }

    }
}