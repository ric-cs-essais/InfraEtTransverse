
using MySql.Data.MySqlClient;

using Transverse.Infrastructure.Persistence.DB.Server.Sql;
using Transverse.Infrastructure.Persistence.DB.Server_;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerSqlRunner : ADBServerSqlRunner<MySqlCommand, MySqlParameter, MySqlDbType>
    {
        public MySqlServerSqlRunner(
            DBServerAccess poDBServerAccess,
            MySqlServerHandler poMySqlServerHandler
        ) : base(poDBServerAccess, poMySqlServerHandler)
        {
        }

    }
}
