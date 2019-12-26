using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Data;
using System.Linq;

namespace User.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new UserContext(serviceProvider
                .GetRequiredService<DbContextOptions<UserContext>>())) {
                if (context.Users.Any()) {
                    return;
                }

                context.Users.AddRange(
                    new User {
                        Name = "Moch. Aji Laksono",
                        Username = "mojila",
                        Password = "ajibro0212"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}