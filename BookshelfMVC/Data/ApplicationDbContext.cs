using BookshelfMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookshelfMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BlogPostModel> BlogPosts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
