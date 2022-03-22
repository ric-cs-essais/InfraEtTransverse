

namespace Transverse.Infrastructure.Server_
{
    public class ServerFactory: AGenericServerFactory<Server>
    {
        private static ServerFactory _oSingletonFactory = null;

        public static ServerFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new ServerFactory();
            }
            return _oSingletonFactory;
        }

        public override Server getServer(string psUrl, uint? piPort = null)
        {
            return new Server(
                this._getUrl(psUrl),
                (piPort == null) ? null : this._getPort((uint)piPort)
            );
        }

        private Url _getUrl(string psUrl)
        {
            return new Url(psUrl);
        }

    }

}
