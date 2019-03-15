using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class AddPostCommand : IRequest<int>
	{
		public string Content { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddPostHandler : IRequestHandler<AddPostCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public AddPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<int> Handle(AddPostCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO post (content, excerpt, title) 
					VALUES (@content, @excerpt, @title)
				RETURNING id
				";
			var postId = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, request, _unitOfWork.Transaction);
			return postId;
		}
	}
}