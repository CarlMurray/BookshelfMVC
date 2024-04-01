﻿namespace BookshelfMVC.ViewModels
{
    public class BlogPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
