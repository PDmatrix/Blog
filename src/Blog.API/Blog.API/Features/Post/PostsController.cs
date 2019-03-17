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
using NSwag.Annotations;

namespace Blog.API.Features.Post
{
	public class PostsController : BaseController
	{
		[HttpGet]
		[ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PostPreviewDto>>> GetAll(int page = 1)
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
	        var addPostCommand = new AddPostCommand
	        {
		        Content = postRequest.Content,
		        Title = postRequest.Title,
		        Excerpt = postRequest.Excerpt
	        };
	        var createdPostId = await Mediator.Send(addPostCommand);
	        return CreatedAtRoute(nameof(GetById),new {Id = createdPostId});
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
	        var updatePostCommand = new UpdatePostCommand
	        {
		        Id = id,
		        Content = postRequest.Content,
		        Title = postRequest.Title,
		        Excerpt = postRequest.Excerpt
	        };
	        await Mediator.Send(updatePostCommand);
	        return NoContent();
        }

        [HttpPost("preview")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [TransactionFree]
        public async Task<ActionResult<PreviewDto>> Preview(PreviewRequest previewRequest)
        {
	        return await Mediator.Send(new PreviewQuery {Content = previewRequest.Content});
        }
	}
}