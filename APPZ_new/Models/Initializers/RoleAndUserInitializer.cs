using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using APPZ_new.Data;
using APPZ_new.Enums;
using APPZ_new.Models;

namespace APPZ_new.Models.Initializers
{
    public class RoleAndUserInitializer
    {
        private static string[] _passwords = { "admin", "ivanko" };
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var mainContext = serviceProvider.GetRequiredService<AppDBContext>();

            if (!identityContext.Roles.Any(r => r.Name == "Admin"))
            {
                identityContext.Roles.Add(new IdentityRole("Admin"));
                identityContext.SaveChanges();
            }
            if (!identityContext.Roles.Any(r => r.Name == "User"))
            {
                identityContext.Roles.Add(new IdentityRole("User"));
                identityContext.SaveChanges();
            }
            
            var UserRole_Admin = identityContext.Roles.First(r => r.Name == "Admin");
            var UserRole_User = identityContext.Roles.First(r => r.Name == "User");


            var superAdmin = GetSuperAdmin();

            if (!identityContext.Users.Any(u => u.UserName == superAdmin.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(superAdmin, _passwords[0]);
                superAdmin.PasswordHash = hashed;

                identityContext.Users.Add(superAdmin);
                identityContext.SaveChanges();

                identityContext.UserRoles.Add(new IdentityUserRole<string>()
                {
                    UserId = superAdmin.Id,
                    RoleId = UserRole_Admin.Id
                });
                identityContext.SaveChanges();

                //var userStore = new UserStore<ApplicationUser>(identityContext);
                //var result = userStore.CreateAsync(superAdmin);

                mainContext.Users.Add(new User { Name = superAdmin.UserName/*, Role = UserRole.SuperAdmin*/ });
                mainContext.SaveChanges();
            }
            //identityContext.SaveChangesAsync();

            var admin = GetAdmin();

            if (!identityContext.Users.Any(u => u.UserName == admin.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(admin, _passwords[1]);
                admin.PasswordHash = hashed;

                identityContext.Users.Add(admin);
                identityContext.SaveChanges();

                identityContext.UserRoles.Add(new IdentityUserRole<string>()
                {
                    UserId = admin.Id,
                    RoleId = UserRole_Admin.Id
                });
                identityContext.SaveChanges();

                //var userStore = new UserStore<ApplicationUser>(identityContext);
                //var result = userStore.CreateAsync(admin);

                mainContext.Users.Add(new User { Name = admin.UserName/*, Role = UserRole.Admin*/ });
                mainContext.SaveChanges();
            }

            //identityContext.SaveChangesAsync();
        }


        private static ApplicationUser GetSuperAdmin()
        {
            return new ApplicationUser
            {
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
        }

        private static ApplicationUser GetAdmin()
        {
            return new ApplicationUser
            {
                Email = "adminivan@mail.com",
                NormalizedEmail = "ADMINIVAN@MAIL.COM",
                UserName = "adminivan@mail.com",
                NormalizedUserName = "ADMINIVAN@MAIL.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
        }
    }
}
