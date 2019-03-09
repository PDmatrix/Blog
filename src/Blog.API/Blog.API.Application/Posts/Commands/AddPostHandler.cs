using System.Threading;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Blog.API.Application.Posts.Models;
using Dapper;
using FluentValidation;
using MediatR;

namespace Blog.API.Application.Posts.Commands
{
	public class AddPost : IRequest<PostDto>
	{
		public string Content { get; set; }
	}
	
	public class AddPostValidator : AbstractValidator<AddPost>
	{
		public AddPostValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddPostHandler : IRequestHandler<AddPost, PostDto>
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		
		public AddPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}
		
		public async Task<PostDto> Handle(AddPost request, CancellationToken cancellationToken)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				const string sql =
					@"
					INSERT INTO post (content) VALUES (@content)
					RETURNING id, content
					";
				unitOfWork.Begin();
				try
				{
					var res = await unitOfWork.Connection.QuerySingleOrDefaultAsync<PostDto>(sql, request, unitOfWork.Transaction);
					unitOfWork.Commit();
					return res;
				}
				catch
				{
					unitOfWork.Rollback();
					throw;
				}
			}
		}
	}
}