using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Queries
{
	public class GetPostQuery : IRequest<PostDto>
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetPostHandler : IRequestHandler<GetPostQuery, PostDto>
	{
		private readonly IConverter<string, string> _converter;
		private readonly IUnitOfWork _unitOfWork;
		
		public GetPostHandler(
			IUnitOfWorkFactory unitOfWorkFactory, 
			IConverter<string, string> converter)
		{
			_converter = converter;
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				SELECT id, content FROM post
				WHERE id = @id
				";
			object sqlParam = new {id = request.Id};
			var post = (await _unitOfWork.Connection.QueryAsync<PostDto>(sql, sqlParam, _unitOfWork.Transaction)).FirstOrDefault();
			return PostSharedLogic.GetConvertedPostDto(post, _converter);
		}

		
	}
}