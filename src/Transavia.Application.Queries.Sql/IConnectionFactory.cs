using System.Data.SqlClient;

namespace Transavia.Application.Queries.Sql
{
    public interface IConnectionFactory
    {
        SqlConnection Create();
    }
}