using System;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API
{
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class Home : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public Home(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		public async Task<ActionResult<GetAllPostsDto>> Get()
		{
			var res = await _mediator.Send(new GetAllPosts());
			return Ok(res);
		}
	}
}