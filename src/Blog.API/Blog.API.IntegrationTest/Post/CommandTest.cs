using System.Net.Http;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Commands;
using Xunit;

namespace Blog.API.IntegrationTest.Post
{
	public class CommandTest : BaseTest
	{
		public CommandTest(TestFactory factory) : base(factory)
		{
		}

		[Theory]
		[InlineData("api/post")]
		public async Task GetHttpRequest(string url)
		{
			var client = Factory.CreateClient();
			var response = await client.PostAsJsonAsync(url, new AddPostCommand {Content = "hooray"});
			response.EnsureSuccessStatusCode();
		}
	}
}