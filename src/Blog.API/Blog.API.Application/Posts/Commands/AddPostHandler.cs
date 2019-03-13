using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class AddPostCommand : IRequest<PostDto>
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddPostHandler : IRequestHandler<AddPostCommand, PostDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public AddPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<PostDto> Handle(AddPostCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO post (content) VALUES (@content)
				RETURNING id, content
				";
			var post = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<PostDto>(sql, request, _unitOfWork.Transaction);
			return post;
		}
	}
}