

namespace Transverse.Infrastructure.Server_
{
    public class ServerAccessFactory: AGenericServerAccessFactory<ServerAccess>
    {
        private static ServerAccessFactory _oSingletonFactory = null;

        public static ServerAccessFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new ServerAccessFactory();
            }
            return _oSingletonFactory;
        }

        public override ServerAccess getServerAccess(string psUrl, string psUserName, string psUserPassword, uint? piPort = null)
        {
            return new ServerAccess(
                this._getServer(psUrl, piPort),
                this._getUserCredentials(psUserName, psUserPassword)
            );
        }

        private Server _getServer(string psUrl, uint? piPort)
        {
            return ServerFactory.getSingleton().getServer(psUrl, piPort);
        }
    }

}
