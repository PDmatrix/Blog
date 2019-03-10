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
	}
	
	// ReSharper disable once UnusedMember.Global
	public class UpdatePostHandler : AsyncRequestHandler<UpdatePostCommand>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public UpdatePostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		protected override async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					UPDATE post SET content = @content
					WHERE id = @id
					";
				await unitOfWork.Connection.ExecuteAsync(sql, request, unitOfWork.Transaction);
			}
		}
	}
}