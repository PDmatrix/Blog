using Xunit;

namespace Blog.API.IntegrationTest
{
	[CollectionDefinition("WebApi collection")]	
	public class WebApiCollection : ICollectionFixture<TestFactory>
	{
		
	}
}