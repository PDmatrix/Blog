using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Models;
using Blog.API.Features.Post;
using Blog.API.IntegrationTest.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Blog.API.IntegrationTest.Post
{
	public class CommandTest : BaseTest
	{
		public CommandTest(TestFactory factory) : base(factory)
		{
		}

		[Theory]
		[InlineData("api/posts")]
		public async Task AddPostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var createdPost = await HttpHandler.CallAsync<PostDto>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, $"{url}/{createdPostObject.Id}"));
			createdPost.Content.Should().Be("<p>foo</p>");
			createdPost.Title.Should().Be("foo");
		}
		
		private class CreatedPost
		{
			public int Id { get; set; }
		}

		private Task<CreatedPost> AddPost(string url)
		{
			var createPostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Post, 
				new PostRequest {Content = "foo", Title = "foo", Excerpt = "foo"}, url);
			return HttpHandler.CallAsync<CreatedPost>(createPostMessage);
		}
		
		[Theory]
		[InlineData("api/posts")]
		public async Task UpdatePostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var updatePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Put,
				new PostRequest {Content = "bar", Title = "bar", Excerpt = "bar"}, $"{url}/{createdPostObject.Id}");
			await HttpHandler.CallAsync(updatePostMessage);
			
			var updatedPost = await HttpHandler.CallAsync<PostDto>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, $"{url}/{createdPostObject.Id}"));
			updatedPost.Content.Should().Be("<p>bar</p>");
			updatedPost.Title.Should().Be("bar");
		}
		
		[Theory]
		[InlineData("api/posts")]
		public async Task DeletePostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var deletePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Delete, $"{url}/{createdPostObject.Id}");
			await HttpHandler.CallAsync(deletePostMessage);

			var allPosts = await HttpHandler.CallAsync<IEnumerable<PostDto>>(
				HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, url));
			allPosts.Should().BeEmpty();
		}
	}
}