using FluentValidation;

namespace Blog.API.Features.Post
{
	public class PreviewRequest
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class PreviewRequestValidator : AbstractValidator<PreviewRequest>
	{
		public PreviewRequestValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
}