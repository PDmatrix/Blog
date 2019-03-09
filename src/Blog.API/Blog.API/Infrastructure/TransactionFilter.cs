using System;
using System.Threading.Tasks;
using Blog.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.API.Infrastructure
{
	public class TransactionFilter : IAsyncActionFilter
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public TransactionFilter(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				try
				{
					unitOfWork.Begin();
					var actionExecuted = await next();
					if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
					{
						unitOfWork.Rollback();
					}
					else
					{
						unitOfWork.Commit();
					}
				}
				catch (Exception)
				{
					unitOfWork.Rollback();
					throw;
				}
			}
		}
	}
}