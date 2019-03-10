using System.Threading.Tasks;
using Xunit;

namespace Blog.API.IntegrationTest.Post
{
	public class QueryTest : BaseTest
	{
		public QueryTest(TestFactory factory) : base(factory)
		{
		}
		
		[Theory]
		[InlineData("api/post")]
		public async Task GetHttpRequest(string url)
		{
			var client = Factory.CreateClient();
			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
		}
	}
}