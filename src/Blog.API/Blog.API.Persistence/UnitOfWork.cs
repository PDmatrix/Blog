using System;
using System.Data;
using Blog.API.Application.Interfaces;

namespace Blog.API.Persistence
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		internal UnitOfWork(IDbConnection connection)
		{
			_id = Guid.NewGuid();
			_connection = connection;
		}

		private readonly IDbConnection _connection;
		private IDbTransaction _transaction;
		private readonly Guid _id;

		IDbConnection IUnitOfWork.Connection => _connection;

		IDbTransaction IUnitOfWork.Transaction => _transaction;

		Guid IUnitOfWork.Id => _id;

		public void Begin()
		{
			_transaction = _connection.BeginTransaction();
		}

		public void Commit()
		{
			_transaction.Commit();
			Dispose();
		}

		public void Rollback()
		{
			_transaction.Rollback();
			Dispose();
		}

		public void Dispose()
		{
			_transaction?.Dispose();
			_transaction = null;
		}
	}
}