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
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IEnumerable<GetAllPostsDto>>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}
		
		public async Task<IEnumerable<GetAllPostsDto>> Handle(GetAllPosts request, CancellationToken ct)
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
				return (await unitOfWork.Connection.QueryAsync<GetAllPostsDto>(sql, sqlParam, unitOfWork.Transaction)).ToList();
			}
		}
	}
}