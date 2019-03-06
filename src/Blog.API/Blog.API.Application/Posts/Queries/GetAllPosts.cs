using System.Collections.Generic;
using Blog.API.Application.Posts.Models;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class GetAllPosts : IRequest<IEnumerable<GetAllPostsDto>>
	{
		public int Page { get; set; } = 1;
	}
}