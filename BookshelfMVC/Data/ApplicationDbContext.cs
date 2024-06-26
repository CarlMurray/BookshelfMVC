﻿using BookshelfMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookshelfMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BlogPostModel> BlogPosts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<BookshelfMVC.DTO.BookDTO> BookDTO { get; set; } = default!;
        public DbSet<BookshelfMVC.ViewModels.BlogPostViewModel> BlogPostViewModel { get; set; } = default!;
    }
}
