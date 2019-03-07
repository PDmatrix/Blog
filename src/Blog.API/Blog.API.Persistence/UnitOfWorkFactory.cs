using System.Data;
using Blog.API.Application.Interfaces;
using Npgsql;

namespace Blog.API.Persistence
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private readonly IDbConnection _connection;

		private UnitOfWorkFactory(IDbConnection connection)
		{
			// Correct mapping of entities with underscores, e.g created_at with CreatedAt
			Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

			_connection = connection;
		}
		
		public UnitOfWorkFactory(string connectionString)
			: this(new NpgsqlConnection(connectionString))
		{}
		
		public IUnitOfWork Create()
		{
            _connection.Open();
            return new UnitOfWork(_connection);
		}
	}
}