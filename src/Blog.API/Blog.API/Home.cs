using System.Threading.Tasks;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion("1.0")]
	public class Home : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public Home(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<GetAllPostsDto>> Get()
		{
			var res = await _mediator.Send(new GetAllPosts());
			return Ok(res);
		}
	}
}