using System.Threading.Tasks;
using Blog.API.Application.Admin.Commands;
using Blog.API.Infrastructure;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blog.API.Features.Admin
{
	public class AdminController : BaseController
	{
		private readonly IOptions<AuthSettings> _options;

		public AdminController(IOptions<AuthSettings> options)
		{
			_options = options;
		}

		[HttpPost("login")]
		[Consumes("application/json")]
		[ProducesDefaultResponseType]
		public async Task<ActionResult<string>> Login(AdminRequest adminRequest)
		{
			var authOptions = _options.Value;
			return await Mediator.Send(new LoginCommand
			{
				UserName = adminRequest.UserName,
				Password = adminRequest.Password,
				Scope = authOptions.Scope,
				ClientId = authOptions.ClientId,
				ClientSecret = authOptions.Secret
			});
		}
	}
}