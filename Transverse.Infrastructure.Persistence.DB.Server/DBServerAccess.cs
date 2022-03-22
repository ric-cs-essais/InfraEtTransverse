using Transverse.Infrastructure.Server_;

using Transverse.Infrastructure.Persistence.DB.Database;


namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerAccess: GenericServerAccess<DBServer>
    {
        public DatabaseName CurrentDatabaseName { get; }

        public DBServerAccess(
            DBServer poServer,
            UserCredentials poUserCredentials,
            DatabaseName poCurrentDatabaseName = null
        ) : base(poServer, poUserCredentials)
        {
            this.CurrentDatabaseName = poCurrentDatabaseName;
        }

    }
}
