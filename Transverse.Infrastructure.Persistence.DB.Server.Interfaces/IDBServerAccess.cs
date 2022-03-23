using Transverse.Infrastructure.Persistence.DB.Database;


namespace Transverse.Infrastructure.Persistence.DB.Server.Interfaces
{
    public interface IDBServerAccess
    {
        DatabaseName CurrentDatabaseName { get; set; }
    }
}
