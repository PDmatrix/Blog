using FluentValidation;

namespace Blog.API.Application.Posts.Models
{
	public class InputPostDto
	{
		public string Content { get; set; }
	}
	
	public class AddPostValidator : AbstractValidator<InputPostDto>
	{
		public AddPostValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
}