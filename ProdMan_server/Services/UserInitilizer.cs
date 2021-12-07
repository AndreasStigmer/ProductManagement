using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_server.Services
{
    public class UserInitilizer : IUserInitilizer
    {
        private readonly ProductDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserInitilizer(ProductDbContext db,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Initialize()
        {

            if (roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult()) return;

            roleManager.CreateAsync(new IdentityRole() { Name = "Admin" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = "Employee" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = "PowerUser" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = "Customer" }).GetAwaiter().GetResult();

            userManager.CreateAsync(new IdentityUser() { UserName = "admin@home.se", Email = "admin@home.se", EmailConfirmed = true }, "Pa$$w0rd").GetAwaiter().GetResult();

            var user = db.Users.FirstOrDefault(u => u.Email == "admin@home.se");
            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

            userManager.CreateAsync(new IdentityUser() { UserName = "emp@home.se", Email = "emp@home.se", EmailConfirmed = true }, "Pa$$w0rd").GetAwaiter().GetResult();

            var user2 = db.Users.FirstOrDefault(u => u.Email == "emp@home.se");
            userManager.AddToRoleAsync(user2, "Employee").GetAwaiter().GetResult();



        }
    }
}
