using BookshelfMVC.Data;
using BookshelfMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlogController> _logger;

        public BlogController(ApplicationDbContext context, ILogger<BlogController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: BlogController
        public ActionResult Blog()
        {
            var blogPosts = _context.BlogPosts;
            var blogPostViewModels = new List<BlogPostViewModel>();
            foreach (var post in blogPosts)
            {
                string authorId = post.ApplicationUserId; 
                var author = _context.Users.FirstOrDefault(u => u.Id == authorId).Email;
                blogPostViewModels.Add(new BlogPostViewModel
                {
                    Title = post.Title,
                    Content = post.Content,
                    Created = post.Created,
                    Author = author
                });
                
                
            }
            ViewData["BlogPosts"] = blogPostViewModels;
            return View();
        }


    }
}
