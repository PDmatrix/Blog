using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Commands;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Features.Post
{
	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class PostController : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public PostController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet]
		[ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll(int page = 1)
		{
			if (page < 1)
				page = 1;
			
			var res = await _mediator.Send(new GetAllPostsQuery {Page = page});
			return res.ToList();
		}
        
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
	        var res = await _mediator.Send(new GetPostQuery {Id = id});
	        if (res == null)
		        return NotFound();
	        
	        return res;
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(PostRequest postRequest)
        {
	        var createdPost = await _mediator.Send(
		        new AddPostCommand {Content = postRequest.Content});
	        return CreatedAtAction(
		        nameof(GetById), 
		        new {id = createdPost.Id}, 
		        createdPost);
        }
        
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
	        await _mediator.Send(new DeletePostCommand {Id = id});
	        return NoContent();
        }
        
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(int id, PostRequest postRequest)
        {
	        await _mediator.Send(
		        new UpdatePostCommand {Id = id, Content = postRequest.Content});
	        return NoContent();
        }
	}
}