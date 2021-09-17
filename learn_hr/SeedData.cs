using learn_hr.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_hr
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);

        }

        public static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByEmailAsync("admin@localhost").Result==null)
            {
                var user = new Employee
                {
                    UserName= "admin@localhost",
                    Email= "admin@localhost",
                    EmailConfirmed=true
                };
                var result = userManager.CreateAsync(user, "Password123*").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name= "Administrator"
                };
                var result=roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Dept-IT").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Dept-IT"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Dept-PS").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Dept-PS"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
