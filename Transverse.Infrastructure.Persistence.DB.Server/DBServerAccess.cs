using Transverse.Infrastructure.Server_;


namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerAccess: GenericServerAccess<DBServer>
    {
        public DBServerAccess(DBServer poServer, UserCredentials poUserCredentials): base(poServer, poUserCredentials)
        {
        }

    }
}
