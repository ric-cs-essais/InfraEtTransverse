using Transverse.Infrastructure.Server_;

using Transverse.Infrastructure.Persistence.DB.Database;
using Transverse.Infrastructure.Persistence.DB.Server.Interfaces;

namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerAccess: GenericServerAccess<DBServer>, IDBServerAccess
    {
        public DatabaseName CurrentDatabaseName { get; set; }

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
