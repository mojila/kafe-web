using Microsoft.EntityFrameworkCore;
using Kafe.Models;

namespace Kafe.Data {
    public class KafeContext : DbContext {
        public KafeContext (DbContextOptions<KafeContext> options) : base(options) {

        }

        public DbSet<Kafe.Models.User> Users { get; set; }
        public DbSet<Kafe.Models.Menu> Menus {get; set;}
    }
}