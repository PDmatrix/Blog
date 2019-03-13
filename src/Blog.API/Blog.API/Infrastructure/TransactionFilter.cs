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
			var method = context.HttpContext.Request.Method;
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				if (method == "GET")
					await ProcessWithoutTransaction(next);
				else
					await ProcessWithTransaction(next, unitOfWork);
			}
		}

		private static async Task ProcessWithTransaction(
			ActionExecutionDelegate next, IUnitOfWork unitOfWork)
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

		private static async Task ProcessWithoutTransaction(ActionExecutionDelegate next)
		{
			await next();
		}
	}
}