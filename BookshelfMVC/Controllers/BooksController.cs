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
using NuGet.Protocol;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [HttpGet]
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> AddBook(BookDTO book, AuthorDTO? author, AuthorBookViewModel model)
        {
            var httpClient = _clientFactory.CreateClient();

            // check if model authorids is null AND if model.author.name is NOT null
            // create a new authordto and post it to api authors endpoint
            // get the id of the new author and add it to the model.authorids list
            if (model.AuthorIds == null && model.Author.Name != null)
            {
                AuthorDTO postAuthor = new AuthorDTO() { Name = model.Author.Name };
                var responseAuthor = await httpClient.PostAsJsonAsync("https://localhost:7108/api/authors", postAuthor);
                var getAuthorId = await responseAuthor.Content.ReadFromJsonAsync<AuthorDTO>();
                model.AuthorIds = new List<int> { getAuthorId.Id };
            }

            BookCreateDTO postBook = new BookCreateDTO(model.Book.Title, model.Book.Description, model.Book.ISBN, model.Book.PublishDate, model.Book.NumPages, model.AuthorIds);
            var response = await httpClient.PostAsJsonAsync("https://localhost:7108/api/books", postBook);

            TempData["SuccessMessage"] = "Book added successfully!";


            return RedirectToAction("AddBook");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook()
        {
            int selectedBookId = Convert.ToInt32(Request.Form["selected-book"]);
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7108/api/books/{selectedBookId}");
            TempData["SuccessMessage"] = "Book deleted successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditBook()
        {
            var selectedBookId = Request.Query["selected-book"];
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7108/api/books/{selectedBookId}");
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive= true };
            BookDTO book =  JsonSerializer.Deserialize<BookDTO>(content, options);
            ViewData["Book"] = book;
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(int bookId)
        {
            var httpClient = _clientFactory.CreateClient();
            bookId = Convert.ToInt32(Request.Query["selected-book"]);

            // Assuming Request.Form.ToJson() serializes the form data into JSON
            // You might need to use a different method to serialize your form data
            var jsonData = Request.Form.ToDictionary();
            BookDTO book = new BookDTO()
            {
                Id = bookId,
                Title = jsonData["book.Title"],
                Description = jsonData["book.Description"],
                ISBN = jsonData["book.ISBN"],
                PublishDate = Convert.ToDateTime(jsonData["book.PublishDate"]),
                NumPages = Convert.ToInt32(jsonData["book.NumPages"]),

            };
            
            
            //BookDTO book = JsonSerializer.Deserialize<BookDTO>(jsonData);

            // Perform HTTP PUT request to update the book resource
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7108/api/books/{bookId}", book);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Redirect to a specific action or route upon successful update
                // You need to specify the action and controller you want to redirect to
                TempData["SuccessMessage"] = "Book editted successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the PUT request fails
                // You may choose to return an error view or take other actions
                return View("Error");
            }
        }

    }
}
