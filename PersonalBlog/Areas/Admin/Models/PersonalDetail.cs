using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Admin.Models
{
    public class PersonalDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortDetail { get; set; }
        public  string  LongDescription{ get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase photo { get; set; }
        public string UplodedAt { get; set; }
        public string UplodedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string updatedBy { get; set; }

    }
}