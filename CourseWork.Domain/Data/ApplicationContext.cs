using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Domain.Data
{
    public class ApplicationContext: IdentityDbContext<User>
    {
        public DbSet<Collection> Collections { get; set; }
        
        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}