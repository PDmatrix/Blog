using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Blog.API.IntegrationTest
{
	public class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;

		public BasicTests(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData("api/post")]
		public async Task GetHttpRequest(string url)
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
		}
	}
}