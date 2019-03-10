using FluentValidation;

namespace Blog.API.Features.Post
{
	public class PostRequest
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class PostRequestValidator : AbstractValidator<PostRequest>
	{
		public PostRequestValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
}