namespace SelfHostWebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SelfHostWebApp.Model.WebAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SelfHostWebApp.Model.WebAppContext context)
        {
            var userManager = new UserManager<MyUser>(new UserStore<MyUser>(context));
            string hash = userManager.PasswordHasher.HashPassword("root@123");
            if (userManager.Find("root", hash) == null)
            {
                userManager.Create(new MyUser() { UserName = "root", Email = "root@email.com"}, hash);
            }
        }
    }
}
