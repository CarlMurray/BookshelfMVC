using BookshelfMVC.Data;
using BookshelfMVC.Models;
using BookshelfMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

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

        [HttpGet("blog/{id:int}")]
        public ActionResult Post(int id)
        {
            var blogPosts = _context.BlogPosts;
            BlogPostModel blogPost = blogPosts.FirstOrDefault(b => b.Id == id);
            string authorId = blogPost.ApplicationUserId;
            var author = _context.Users.FirstOrDefault(u => u.Id == authorId).Email;
            var blogPostViewModel = new BlogPostViewModel
            {
                Id=blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                Created = blogPost.Created,
                Author = author
            };
            ViewData["Post"] = blogPostViewModel;
            return View(blogPostViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPost() { 
            return View(); 
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddPost(BlogPostViewModel blogPost=null)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            BlogPostModel blogPostModel = new BlogPostModel()
            {
                ApplicationUserId = userId,
                Title = blogPost.Title,
                Content = blogPost.Content,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };
            _context.BlogPosts.Add(blogPostModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");          
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            BlogPostModel b = _context.BlogPosts.Where(b => b.Id == id).FirstOrDefault();
            _context.BlogPosts.Remove(b);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(int id)
        {
            BlogPostModel blogPost = _context.BlogPosts.Where(b=> b.Id == id).FirstOrDefault();
            BlogPostViewModel blogPostViewModel = new BlogPostViewModel()
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                Created = DateTime.Now,

            };
            return View(blogPostViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditPost()
        {

            var form = Request.Form;
            var blogPost = _context.BlogPosts.Where(b => b.Id == Convert.ToInt32(form["Id"])).FirstOrDefault();
            blogPost.Title = form["Title"];
            blogPost.Content = form["Content"];
            blogPost.Created = DateTime.Parse(form["Created"]);
            blogPost.Updated = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Post", new { id = blogPost.Id });
        }
    }
}
