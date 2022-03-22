using Transverse.Infrastructure;
using Transverse.Infrastructure.Server_;

namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServer: GenericServer<DBServerUrl>
    {
        public DBServer(DBServerUrl poDBServerUrl, Port poServerPort = null) : base(poDBServerUrl, poServerPort)
        {
        }
    }
}
