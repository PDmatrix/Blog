using Xunit;

namespace Blog.API.IntegrationTest.Infrastructure
{
	[CollectionDefinition("WebApi collection")]	
	public class WebApiCollection : ICollectionFixture<TestFactory>
	{
		
	}
}