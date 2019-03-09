using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class GetPost : IRequest<PostDto>
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetPostHandler : IRequestHandler<GetPost, PostDto>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		public GetPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}
		
		public async Task<PostDto> Handle(GetPost request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					SELECT id, content FROM post
					WHERE id = @id
					";
				object sqlParam = new {id = request.Id};
				return (await unitOfWork.Connection.QueryAsync<PostDto>(sql, sqlParam, unitOfWork.Transaction)).FirstOrDefault();
			}
		}
	}
}