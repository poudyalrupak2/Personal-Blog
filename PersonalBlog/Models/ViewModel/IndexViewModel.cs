using PersonalBlog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.Models.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<News> news { get; set; }
        public IEnumerable<News> SportNews { get; set; }
        public IEnumerable<Achievement> Acheivement { get; set; }
        public IEnumerable<Gallery> galleries { get; set; }
        public IEnumerable<History> histories { get; set; }
    }
}