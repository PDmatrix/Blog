using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class AddPostCommand : InputPostDto, IRequest<PostDto>
	{
		
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddPostHandler : IRequestHandler<AddPostCommand, PostDto>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		
		public AddPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}
		
		public async Task<PostDto> Handle(AddPostCommand request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					INSERT INTO post (content) VALUES (@content)
					RETURNING id, content
					";
				var res = await unitOfWork.Connection.QuerySingleOrDefaultAsync<PostDto>(sql, request, unitOfWork.Transaction);
				return res;
			}
		}
	}
}