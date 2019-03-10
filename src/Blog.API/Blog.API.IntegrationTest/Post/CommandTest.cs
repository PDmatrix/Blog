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
		[InlineData("api/post")]
		public async Task AddPostTheory(string url)
		{
			var createdPost = await AddPost(url);
			createdPost.Content.Should().Be("foo");
		}

		private async Task<PostDto> AddPost(string url)
		{
			var createPostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Post, 
				new PostRequest {Content = "foo"}, url);
			return await HttpHandler.CallAsync<PostDto>(createPostMessage);
		}
		
		[Theory]
		[InlineData("api/post")]
		public async Task UpdatePostTheory(string url)
		{
			var createdPost = await AddPost(url);
			var updatePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Put,
				new PostRequest {Content = "bar"}, $"{url}/{createdPost.Id}");
			await HttpHandler.CallAsync(updatePostMessage);
			
			var updatedPost = await HttpHandler.CallAsync<PostDto>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, $"{url}/{createdPost.Id}"));
			updatedPost.Content.Should().Be("bar");
		}
		
		[Theory]
		[InlineData("api/post")]
		public async Task DeletePostTheory(string url)
		{
			var createdPost = await AddPost(url);
			var deletePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Delete, $"{url}/{createdPost.Id}");
			await HttpHandler.CallAsync(deletePostMessage);

			var allPosts = await HttpHandler.CallAsync<IEnumerable<PostDto>>(
				HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, url));
			allPosts.Should().BeEmpty();
		}
	}
}