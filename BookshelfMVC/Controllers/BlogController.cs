using BookshelfMVC.Data;
using BookshelfMVC.Models;
using BookshelfMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        [HttpGet]
        public ActionResult Index()
        {
            var blogPosts = _context.BlogPosts;
            var blogPostViewModels = new List<BlogPostViewModel>();
            foreach (var post in blogPosts)
            {
                string authorId = post.ApplicationUserId; 
                var author = _context.Users.FirstOrDefault(u => u.Id == authorId).Email;
                blogPostViewModels.Add(new BlogPostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Created = post.Created,
                    Author = author
                });
                
                
            }
            ViewData["BlogPosts"] = blogPostViewModels;
            return View();
        }

        [HttpGet("blog/{id}")]
        public ActionResult Post(int id)
        {
            var blogPosts = _context.BlogPosts;
            BlogPostModel blogPost = blogPosts.FirstOrDefault(b => b.Id == id);
            string authorId = blogPost.ApplicationUserId;
            var author = _context.Users.FirstOrDefault(u => u.Id == authorId).Email;
            var blogPostViewModel = new BlogPostViewModel
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
                Created = blogPost.Created,
                Author = author
            };
            ViewData["Post"] = blogPostViewModel;
            return View(blogPostViewModel);
        }


    }
}
