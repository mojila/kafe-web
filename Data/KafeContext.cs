using Microsoft.EntityFrameworkCore;
using KafeWeb.Models;

namespace KafeWeb.Data {
    public class KafeContext : DbContext {
        public KafeContext (DbContextOptions<KafeContext> options) : base(options) {

        }

        public DbSet<KafeWeb.Models.User> Users { get; set; }
        public DbSet<KafeWeb.Models.Menu> Menus {get; set;}

        public DbSet<KafeWeb.Models.Order> Orders {get; set;}

        public DbSet<KafeWeb.Models.Table> Tables {get; set;}
    }
}