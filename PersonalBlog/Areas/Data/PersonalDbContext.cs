using PersonalBlog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonalBlog.Areas.Data
{
    public class PersonalDbContext:DbContext
    {
       public PersonalDbContext():base("defaultconnection")
        {

        }
        public DbSet<History> Histories { get; set; }
        public DbSet<Login> login { get; set; }

        public System.Data.Entity.DbSet<PersonalBlog.Areas.Admin.Models.PersonalDetail> PersonalDetails { get; set; }

        public System.Data.Entity.DbSet<PersonalBlog.Areas.Admin.Models.News> News { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Images> Images { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Images>()
                   .HasOptional(j => j.Gallery)
                   .WithMany()
                   .WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}