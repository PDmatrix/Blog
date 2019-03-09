using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Dapper;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class DeletePost : IRequest
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class DeletePostHandler : AsyncRequestHandler<DeletePost>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public DeletePostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		protected override async Task Handle(DeletePost request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					DELETE FROM post
					WHERE id = @id
					";
				await unitOfWork.Connection.ExecuteAsync(sql, new {id = request.Id}, unitOfWork.Transaction);
			}
		}
	}
}