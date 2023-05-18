using MaliLb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaliLb
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<Edition> Edition { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Visitor> Visitor { get; set; }

        public ApplicationDbContext() => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    }
}
