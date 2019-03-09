using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Blog.API.Features
{
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class Posts : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public Posts(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllPostsDto>> Get([FromQuery] int page = 1)
		{
			if (page < 1)
				page = 1;
			
			var res = await _mediator.Send(new GetAllPosts {Page = page});
			return res;
		}
	}
}