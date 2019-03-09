namespace Blog.API.Domain.Entities
{
    public class Post : Entity<int>
    {
        public string Content { get; set; }
    }
}
