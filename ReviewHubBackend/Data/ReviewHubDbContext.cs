using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Models;

namespace ReviewHubBackend.Data
{
    public class ReviewHubDbContext : DbContext
    {
        public ReviewHubDbContext(DbContextOptions<ReviewHubDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
