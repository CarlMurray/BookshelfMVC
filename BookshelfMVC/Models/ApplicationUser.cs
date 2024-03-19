using Microsoft.AspNetCore.Identity;

namespace BookshelfMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<BlogPostModel> BlogPosts { get; } = new List<BlogPostModel>();
    }
}
