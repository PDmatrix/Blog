using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class UpdatePostCommand : IRequest
	{
		public int Id { get; set; }
		public string Content { get; set; }	
		public string Title { get; set; }
		public string Excerpt { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class UpdatePostHandler : AsyncRequestHandler<UpdatePostCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public UpdatePostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				UPDATE post SET content = @content, excerpt = @excerpt, title = @title
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, request, _unitOfWork.Transaction);
		}
	}
}