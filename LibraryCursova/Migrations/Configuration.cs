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

            // ������� ��� ����
            var adminRole = new IdentityRole { Name = "admin" };
            var workerRole = new IdentityRole { Name = "user" };
            var userRole = new IdentityRole { Name = "worker" };

            // ��������� ���� � ��
            roleManager.Create(adminRole);
            roleManager.Create(workerRole);
            roleManager.Create(userRole);

            // ������� �������������
            var admin = new ApplicationUser { Email = "Admin@gmail.com", UserName = "Admin@gmail.com", };
            string passwordAdmin = "Admin_123";
            var resultAdmin = userManager.Create(admin, passwordAdmin);
            var worker = new ApplicationUser { Email = "Worker@gmail.com", UserName = "Worker@gmail.com", };
            string passwordWorker = "Worker_123";
            var resultWorker = userManager.Create(worker, passwordWorker);
            var user = new ApplicationUser { Email = "User@gmail.com", UserName = "User@gmail.com", };
            string passwordUser = "User_123";
            var resultUser = userManager.Create(user, passwordUser);

            context.Companies.Add(new Company { Name = "��������� �������" });
            context.Books.Add(new Book { Name = "����� � ���", AvtorName = "�. �������", Price = 220, Vudavnuctvo = "�������", YearOfVudannya = 1812, Genre = "�����-������", CompanyId = 0, Count = 5});
            context.Books.Add(new Book { Name = "���� � ����", AvtorName = "�. ��������", Price = 180, Vudavnuctvo = "�����", YearOfVudannya = 1883, Genre = "�����", CompanyId = 0, Count = 5 });
            context.Books.Add(new Book { Name = "�����", AvtorName = "�. �����", Price = 150, Vudavnuctvo = "�����", YearOfVudannya = 1955, Genre = "�����", CompanyId = 0, Count = 5 });

            // ���� �������� ������������ ������ �������
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
