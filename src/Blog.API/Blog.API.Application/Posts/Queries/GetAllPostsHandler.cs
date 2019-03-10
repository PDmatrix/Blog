using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class GetAllPostsQuery : IRequest<IEnumerable<PostDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}
		
		public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken ct)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					SELECT id, content FROM post
					LIMIT @pageSize OFFSET @page
					";
				const int pageSize = 10;
				object sqlParam = new {pageSize, page = (request.Page - 1) * pageSize };
				return (await unitOfWork.Connection.QueryAsync<PostDto>(sql, sqlParam, unitOfWork.Transaction)).ToList();
			}
		}
	}
}