namespace PersonalBlog.Migrations
{
    using PersonalBlog.Areas.Admin.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonalBlog.Areas.Data.PersonalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonalBlog.Areas.Data.PersonalDbContext context)
        {
            byte[] passwordhash;
            byte[] passwordsalt;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                string password = "nepalnepal1";
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordsalt = hmac.Key;
            }
            context.login.AddOrUpdate(x => x.Id,
           new Login
           {
               Id = 1,
               Email = "poudyalrupak2@gmail.com",
               Role = "SuperAdmin",
               PasswordHash = passwordhash,
               PasswordSalt = passwordsalt
           },
            new Login
            {
                Id = 2,
                Email = "raj2@gmail.com",
                Role = "Admin",
                PasswordHash = passwordhash,
                PasswordSalt = passwordsalt
            });
            
         

        }
    }
}
