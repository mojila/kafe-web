using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kafe.Data;
using System.Linq;
using Kafe.Models;

namespace Kafe.Models {
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new KafeContext(serviceProvider
                .GetRequiredService<DbContextOptions<KafeContext>>())) {
                if (context.Users.Any() && context.Menus.Any()) {
                    return;
                }

                context.Users.AddRange(
                    new User {
                        Name = "Moch. Aji Laksono",
                        Username = "mojila",
                        Password = "ajibro0212"
                    }
                );

                context.Menus.AddRange(
                    new Menu {
                        Name = "Cappucino",
                        Picture = "",
                        Price = 12000
                    }
                );

                context.SaveChanges();
            }
        }
    }   
}