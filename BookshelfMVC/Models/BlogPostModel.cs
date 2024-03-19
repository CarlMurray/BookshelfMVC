using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookshelfMVC.Models
{
    [Table("BlogPosts")]
    public class BlogPostModel
    {
        
        [Key][Required]public int Id { get; set; }
        [Required][MaxLength(200)]public string Title { get; set; }
        [Required][MaxLength(5000)]public string Content { get; set; }
        [Required]public DateTime Created { get; set; }
        [Required]public DateTime Updated { get; set; }
    }
}
