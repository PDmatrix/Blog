using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Commands;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Blog.API.Features
{
	[ApiController]
	[Produces("application/json")]
	[Consumes("application/json")]
	[Route("api/[controller]")]
	public class Post : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public Post(IMediator mediator)
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
			
			var res = await _mediator.Send(new GetAllPosts {Page = page});
			return res.ToList();
		}
        
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
	        var res = await _mediator.Send(new GetPost {Id = id});
	        if (res == null)
		        return NotFound();
	        
	        return res;
        }
        
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(AddPostCommand addPostCommand)
        {
	        var createdPost = await _mediator.Send(addPostCommand);
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
	        await _mediator.Send(new DeletePost {Id = id});
	        return NoContent();
        }
        
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(int id, InputPostDto inputPostDto)
        {
	        await _mediator.Send(new UpdatePost {Id = id, Content = inputPostDto.Content});
	        return NoContent();
        }
	}
}