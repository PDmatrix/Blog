using FluentValidation;

namespace Blog.API.Features.Post
{
	public class PostRequest
	{
		public string Content { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class PostRequestValidator : AbstractValidator<PostRequest>
	{
		public PostRequestValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
			RuleFor(r => r.Title).NotEmpty();
			RuleFor(r => r.Excerpt).NotEmpty();
		}
	}
}