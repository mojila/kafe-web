using Microsoft.EntityFrameworkCore;
using Kafe.Models;

namespace Kafe.Data {
    public class KafeContext : DbContext {
        public KafeContext (DbContextOptions<KafeContext> options) : base(options) {

        }

        public DbSet<User> Users { get; set; }
    }
}