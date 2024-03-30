using BookshelfMVC.DTO;
using System.Text.Json.Serialization;

namespace BookshelfMVC.ViewModels
{
    public class AuthorBookViewModel
    {
        [JsonIgnore]
        public AuthorDTO Author { get; set; }
        public BookDTO Book { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
