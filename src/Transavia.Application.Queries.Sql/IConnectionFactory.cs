using System.Data.SqlClient;

namespace Transavia.Application.Queries.Sql
{
    public interface IConnectionFactory
    {
        SqlConnection Create();
    }

    public sealed class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}