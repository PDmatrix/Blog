namespace Blog.API.Domain.Entities
{
    public class Post : Entity<int>
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
    }
}
