using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookshelfMVC.DTO
{

    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public int NumPages { get; set; }
        public List<AuthorDTO> Authors { get; set; }
    }
}