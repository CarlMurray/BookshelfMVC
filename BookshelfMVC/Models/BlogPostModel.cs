using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookshelfMVC.Models
{
    [Table("BlogPosts")]
    public class BlogPostModel
    {

        [Key][Required] public int Id { get; set; }
        [Required] public required string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        [Required][MaxLength(200)] public required string Title { get; set; }
        [Required][MaxLength(5000)] public required string Content { get; set; }
        [Required] public DateTime Created { get; set; }
        [Required] public DateTime Updated { get; set; }
    }
}
