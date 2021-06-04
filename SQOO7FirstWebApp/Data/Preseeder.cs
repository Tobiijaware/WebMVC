using Microsoft.AspNetCore.Identity;
using SQOO7FirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Data
{
    static public class Preseeder
    {
        public static void SeedIt(SQOO7DbContext context, 
            UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string[] RoleList = { "Manager", "Admin", "regular" };
            string[] ClaimsList = { "CanEdit", "CanDelete", "CanUpdate" };

            // preseed roles
            if (!context.Roles.Any())
            {
                for (var i = 0; i < RoleList.Length; i++)
                {
                    var role = new IdentityRole(RoleList[i]);
                    roleManager.CreateAsync(role);
                }
            }

            // preseed claims
            //if (!context.ClaimsList.Any())
            //{
            //    for (var i = 0; i < RoleList.Length; i++)
            //    {
            //        var role = new IdentityRole(RoleList[i]);
            //        roleManager.CreateAsync(role);
            //    }
            //}
        }
    }
}
