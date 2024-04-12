using BookshelfMVC.Data;
using BookshelfMVC.DTO;
using BookshelfMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BookshelfMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            HttpClient httpClient = _clientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7108/api/books");
            Stream responseString = await response.Content.ReadAsStreamAsync();
            JsonNode responseNode = await JsonNode.ParseAsync(responseString);
            JsonNode dataNode = responseNode["data"];
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookDTO> books = JsonSerializer.Deserialize<List<BookDTO>>(dataNode, options);
            _ = books.ToArray();
            ViewData["Books"] = books;
            _ = _context.SaveChanges();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
