using System.Data;

namespace Persistencia.DapperConnection
{
    public interface IFactoryConnection
    {
         void CloseConnection();
         IDbConnection GetConnection();
    }
}