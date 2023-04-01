using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HamburgerMVC.Models
{
    public class SeedData
    {
//        public static async Task Initialize(IServiceProvider serviceProvider)
//        {
//            var context = serviceProvider.GetService<Context>();

//            //string managerRole = "Manager";
//            //        private RoleManager<IdentityRole> roleManager;
//            //private UserManager<AppUser> userManager;

//            //public SeedData(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager)
//            //{
//            //    roleManager = _roleManager;
//            //    userManager = _userManager;
//            //}

//            string[] roles = new string[] { "Manager" };

//            foreach (string role in roles)
//            {
//                var roleStore = new RoleStore<IdentityRole>(context);

//                if (!context.Roles.Any(r => r.Name == role))
//                {
//                    roleStore.CreateAsync(new IdentityRole(role));
//                }
//            }

//            var user = new AppUser
//            {
//                UserName = "Manager007",
//                Email = "manager@example.com",
//                NormalizedEmail = "MANAGER@EXAMPLE.COM",
//                NormalizedUserName = "MANAGER007",
//                PhoneNumber = "+111111111111",
//                EmailConfirmed = true,
//                PhoneNumberConfirmed = true,
//                SecurityStamp = Guid.NewGuid().ToString("D"),
//            };

//            if (!context.Users.Any(u => u.UserName == user.UserName))
//            {
//                var password = new PasswordHasher<AppUser>();
//                var hashed = password.HashPassword(user, "secret");
//                user.PasswordHash = hashed;

//                var userStore = new UserStore<AppUser>(context);
//                var result = userStore.CreateAsync(user);

//            }

//            AssignRoles(serviceProvider, user.Email, roles);

//            context.SaveChangesAsync();

////            if (await roleManager.FindByNameAsync(managerRole) == null)
////            {
////                await roleManager.CreateAsync(new IdentityRole(managerRole));
////            }


////            if (await userManager.FindByEmailAsync("manager@example.com") == null)
////            {
////                var user = new IdentityUser
////                {
////                    UserName = "manager@example.com",
////                    Email = "manager@example.com",
////                    EmailConfirmed = true,
////                };



////    var result = await userManager.CreateAsync(user, "Manager1234.");
////                if (result.Succeeded)
////                {
////                    await userManager.AddToRoleAsync(user, managerRole);
////}
////            }


//        }
//        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
//        {
//            UserManager<AppUser> _userManager = services.GetService<UserManager<AppUser>>();
//            AppUser user = await _userManager.FindByEmailAsync(email);
//            var result = await _userManager.AddToRolesAsync(user, roles);

//            return result;
//        }
    }
}
