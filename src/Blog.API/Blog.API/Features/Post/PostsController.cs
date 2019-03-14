using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.API.Application.Posts.Commands;
using Blog.API.Application.Posts.Models;
using Blog.API.Application.Posts.Queries;
using Blog.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Features.Post
{
	
	public class PostsController : BaseController
	{
		[HttpGet]
		[ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll(int page = 1)
		{
			if (page < 1)
				page = 1;
			
			var res = await Mediator.Send(new GetAllPostsQuery {Page = page});
			return res.ToList();
		}
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
	        var res = await Mediator.Send(new GetPostQuery {Id = id});
	        if (res == null)
		        return NotFound();
	        
	        return res;
        }
        
        [Authorize]
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> Create(PostRequest postRequest)
        {
	        var createdPost = await Mediator.Send(
		        new AddPostCommand {Content = postRequest.Content});
	        return CreatedAtAction(
		        nameof(GetById), 
		        new {id = createdPost.Id}, 
		        createdPost);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
	        await Mediator.Send(new DeletePostCommand {Id = id});
	        return NoContent();
        }
        
        [Authorize]
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<ActionResult> Update(int id, PostRequest postRequest)
        {
	        await Mediator.Send(
		        new UpdatePostCommand {Id = id, Content = postRequest.Content});
	        return NoContent();
        }

        [HttpPost("preview")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [TransactionFree]
        public async Task<ActionResult<string>> Preview(PostRequest postRequest)
        {
	        return await Mediator.Send(new PreviewQuery {Content = postRequest.Content});
        }
	}
}