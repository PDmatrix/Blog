namespace Blog.API.Infrastructure
{
	public class AuthSettings
	{
		public string Authority { get; set; }
		
		public string Secret { get; set; }

		public string Scope { get; set; }

		public string ClientId { get; set; }
	}
}