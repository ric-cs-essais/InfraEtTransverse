
using Transverse.Infrastructure.Server_;


namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerFactory: AGenericServerFactory<DBServer>
    {
        private static DBServerFactory _oSingletonFactory = null;

        public static DBServerFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new DBServerFactory();
            }
            return _oSingletonFactory;
        }

        public override DBServer getServer(string psUrl, uint? piPort = null)
        {
            return new DBServer(
                this._getUrl(psUrl),
                (piPort == null) ? null : this._getPort((uint)piPort)
            );
        }

        private DBServerUrl _getUrl(string psUrl)
        {
            return new DBServerUrl(psUrl);
        }

    }

}
