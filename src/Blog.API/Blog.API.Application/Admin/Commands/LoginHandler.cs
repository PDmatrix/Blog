using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using MediatR;

namespace Blog.API.Application.Admin.Commands
{
	public class LoginCommand : IRequest<string>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		
		public string ClientId { get; set; }
		public string Scope { get; set; }
		public string ClientSecret { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class LoginHandler : IRequestHandler<LoginCommand, string>
	{
		private readonly HttpClient _client;
		
		public LoginHandler(IHttpClientFactory clientFactory)
		{
			_client = clientFactory.CreateClient("identity");
		}
		
		public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var discoveryDocument = await _client.GetDiscoveryDocumentAsync(cancellationToken: cancellationToken);
			var tokenResponse = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
			{
				Address = discoveryDocument.TokenEndpoint,
				ClientId = request.ClientId,
				ClientSecret = request.ClientSecret,
				
				UserName = request.UserName,
				Password = request.Password,
				Scope = request.Scope
			}, cancellationToken);
			return tokenResponse.AccessToken;
		}
	}
}