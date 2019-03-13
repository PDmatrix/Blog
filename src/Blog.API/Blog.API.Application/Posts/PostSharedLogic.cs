using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;

namespace Blog.API.Application.Posts
{
	public static class PostSharedLogic
	{
		public static PostDto GetConvertedPostDto(PostDto post, IConverter<string, string> converter)
		{
			if (post == null)
				return null;
			
			post.Content = converter.Convert(post.Content);
			return post;
		}
	}
}