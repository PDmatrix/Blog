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
		private readonly IConverter<string, string> _converter;
		private readonly IUnitOfWork _unitOfWork;
		
		public GetAllPostsHandler(
			IUnitOfWorkFactory unitOfWorkFactory, 
			IConverter<string, string> converter)
		{
			_converter = converter;
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken ct)
		{
			const string sql =
				@"
				SELECT id, content FROM post
				LIMIT @pageSize OFFSET @page
				";
			const int pageSize = 10;
			object sqlParam = new {pageSize, page = (request.Page - 1) * pageSize };
			var posts = (await _unitOfWork.Connection.QueryAsync<PostDto>(sql, sqlParam, _unitOfWork.Transaction)).ToList();
			return posts.Select(r => PostSharedLogic.GetConvertedPostDto(r, _converter));
		}
	}
}