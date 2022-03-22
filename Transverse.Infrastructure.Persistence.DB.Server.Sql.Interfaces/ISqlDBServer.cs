using Transverse.Infrastructure.Persistence.DB.Database;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces
{
    public interface ISqlDBServer
    {
        DatabaseName CurrentDatabaseName { get; }
    }
}
