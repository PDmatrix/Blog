using FluentValidation;

namespace Blog.API.Features.Admin
{
	public class AdminRequest
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
	
	public class AdminRequestValidator : AbstractValidator<AdminRequest>
	{
		public AdminRequestValidator()
		{
			RuleFor(r => r.UserName).NotEmpty();
			RuleFor(r => r.Password).NotEmpty();
		}
	}
}