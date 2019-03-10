using System.Threading.Tasks;
using Xunit;

namespace Blog.API.IntegrationTest
{
	[Collection("WebApi collection")]
	public class BaseTest : IAsyncLifetime
	{
		protected readonly TestFactory Factory;

		protected BaseTest(TestFactory factory)
		{
			Factory = factory;
		}

		
		public Task InitializeAsync() => Task.CompletedTask;

		public async Task DisposeAsync()
		{
			await Factory.ResetCheckpoint();
		}
	}
}