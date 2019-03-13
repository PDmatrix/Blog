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
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IConverter<string, string> _converter;
		public GetPostHandler(
			IUnitOfWorkFactory unitOfWorkFactory, 
			IConverter<string, string> converter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_converter = converter;
		}
		
		public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					SELECT id, content FROM post
					WHERE id = @id
					";
				object sqlParam = new {id = request.Id};
				var post = (await unitOfWork.Connection.QueryAsync<PostDto>(sql, sqlParam, unitOfWork.Transaction)).FirstOrDefault();
				return PostSharedLogic.GetConvertedPostDto(post, _converter);
			}
		}

		
	}
}