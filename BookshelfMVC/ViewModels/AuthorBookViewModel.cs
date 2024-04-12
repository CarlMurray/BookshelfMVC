using BookshelfMVC.DTO;
using System.Text.Json.Serialization;

namespace BookshelfMVC.ViewModels
{
    public class AuthorBookViewModel
    {
        [JsonIgnore]
        public required AuthorDTO Author { get; set; }
        public required BookDTO Book { get; set; }
        public required List<int> AuthorIds { get; set; }
    }
}
