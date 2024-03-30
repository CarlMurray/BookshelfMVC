using BookshelfMVC.Data;
using BookshelfMVC.DTO;
using BookshelfMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using BookshelfMVC.ViewModels;
using System.Text;

namespace BookshelfMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AllBooks()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7108/api/books");
            var responseString = await response.Content.ReadAsStreamAsync();
            JsonNode responseNode = await JsonNode.ParseAsync(responseString);
            JsonNode dataNode = responseNode["data"];
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookDTO> books = JsonSerializer.Deserialize<List<BookDTO>>(dataNode, options);
            BookDTO[] booksArray = books.ToArray();
            ViewData["Books"] = books;
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7108/api/authors");
            var responseString = await response.Content.ReadAsStreamAsync();
            JsonNode responseNode = await JsonNode.ParseAsync(responseString);
            JsonNode dataNode = responseNode["data"];
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AuthorDTO> authors = JsonSerializer.Deserialize<List<AuthorDTO>>(dataNode, options);
            AuthorDTO[] authorsArray = authors.ToArray();
            ViewData["Authors"] = authors; 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO book, AuthorDTO author, AuthorBookViewModel model)
        {
            var httpClient = _clientFactory.CreateClient();
            BookCreateDTO postBook = new BookCreateDTO(model.Book.Title, model.Book.Description, model.Book.ISBN, model.Book.PublishDate, model.Book.NumPages, model.AuthorIds);
            var response = await httpClient.PostAsJsonAsync("https://localhost:7108/api/books", postBook);
    

   
            return Ok();
        }   
    }
}
