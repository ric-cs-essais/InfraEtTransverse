
using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;

using Transverse.Infrastructure.Persistence.DB.Database;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql
{
    public class SqlDBServer : DBServer, ISqlDBServer
    {
        public DatabaseName CurrentDatabaseName { get; }

        public SqlDBServer(
            DBServerUrl poDBServerUrl, 
            DatabaseName poCurrentDatabaseName = null,
            Port poServerPort = null
        ) : base(poDBServerUrl, poServerPort)
        {
            this.CurrentDatabaseName = poCurrentDatabaseName;
        }

        
    }
}
