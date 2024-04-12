namespace BookshelfMVC.ViewModels
{
    public class BlogPostViewModel
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
