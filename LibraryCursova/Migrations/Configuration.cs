namespace LibraryCursova.Migrations
{
    using LibraryCursova.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryCursova.Models.BookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryCursova.Models.BookContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var adminRole = new IdentityRole { Name = "admin" };
            var workerRole = new IdentityRole { Name = "user" };
            var userRole = new IdentityRole { Name = "worker" };

            // добавляем роли в бд
            roleManager.Create(adminRole);
            roleManager.Create(workerRole);
            roleManager.Create(userRole);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "Admin@gmail.com", UserName = "Admin@gmail.com", };
            string passwordAdmin = "Admin_123";
            var resultAdmin = userManager.Create(admin, passwordAdmin);
            var worker = new ApplicationUser { Email = "Worker@gmail.com", UserName = "Worker@gmail.com", };
            string passwordWorker = "Worker_123";
            var resultWorker = userManager.Create(worker, passwordWorker);
            var user = new ApplicationUser { Email = "User@gmail.com", UserName = "User@gmail.com", };
            string passwordUser = "User_123";
            var resultUser = userManager.Create(user, passwordUser);

            context.Companies.Add(new Company { Name = "Книжковий магазин" });
            context.Books.Add(new Book { Name = "Война и мир", AvtorName = "Л. Толстой", Price = 220, Vudavnuctvo = "Сонечко", YearOfVudannya = 1812, Genre = "Роман-епопея", CompanyId = 0, Count = 5});
            context.Books.Add(new Book { Name = "Отцы и дети", AvtorName = "И. Тургенев", Price = 180, Vudavnuctvo = "Голуб", YearOfVudannya = 1883, Genre = "Роман", CompanyId = 0, Count = 5 });
            context.Books.Add(new Book { Name = "Чайка", AvtorName = "А. Чехов", Price = 150, Vudavnuctvo = "Лебідь", YearOfVudannya = 1955, Genre = "Роман", CompanyId = 0, Count = 5 });

            // если создание пользователя прошло успешно
            if (resultUser.Succeeded)
            {
                userManager.AddToRole(user.Id, userRole.Name);
            }
            if (resultWorker.Succeeded)
            {
                userManager.AddToRole(worker.Id, workerRole.Name);
            }
            if (resultAdmin.Succeeded)
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
            }
        }
    }
}
