//using firsttask.Models.Helpers;
//using Microsoft.AspNetCore.Identity;

//namespace firsttask.Data
//{
//    public class AppDbInitializer
//    {
//        public static async Task SeedRolesToDb(IApplicationBuilder applicationBuilder)
//        {
//            using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var roleManeger = servicesScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//                if (!await roleManeger.RoleExistsAsync(UserRoles.Maneger))
//                    await roleManeger.CreateAsync(new IdentityRole(UserRoles.Maneger));
//                if (!await roleManeger.RoleExistsAsync(UserRoles.Customer))
//                    await roleManeger.CreateAsync(new IdentityRole(UserRoles.Customer));
//            }
//        }
//    }
//}
