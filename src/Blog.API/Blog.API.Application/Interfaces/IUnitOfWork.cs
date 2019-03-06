using System;
using System.Data;

namespace Blog.API.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		Guid Id { get; }
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; }
		void Begin();
		void Commit();
		void Rollback();
	}
}