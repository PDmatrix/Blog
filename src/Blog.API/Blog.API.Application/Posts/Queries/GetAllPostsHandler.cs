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
	public class GetAllPostsQuery : IRequest<IEnumerable<PostPreviewDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostPreviewDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<IEnumerable<PostPreviewDto>> Handle(GetAllPostsQuery request, CancellationToken ct)
		{
			const string sql =
				@"
				SELECT id, title, excerpt FROM post
				LIMIT @pageSize OFFSET @page
				";
			const int pageSize = 10;
			object sqlParam = new
			{
				pageSize, 
				page = (request.Page - 1) * pageSize
			};
			return (await _unitOfWork.Connection.QueryAsync<PostPreviewDto>(sql, sqlParam, _unitOfWork.Transaction)).ToList();
		}
	}
}