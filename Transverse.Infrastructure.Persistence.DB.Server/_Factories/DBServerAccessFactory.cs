using Transverse.Infrastructure.Server_;


namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerAccessFactory: AGenericServerAccessFactory<DBServerAccess>
    {
        private static DBServerAccessFactory _oSingletonFactory = null;

        public static DBServerAccessFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new DBServerAccessFactory();
            }
            return _oSingletonFactory;
        }

        public override DBServerAccess getServerAccess(string psUrl, string psUserName, string psUserPassword, uint? piPort = null)
        {
            return new DBServerAccess(
                this._getServer(psUrl, piPort),
                this._getUserCredentials(psUserName, psUserPassword)
            );
        }

        private DBServer _getServer(string psUrl, uint? piPort)
        {
            return DBServerFactory.getSingleton().getServer(psUrl, piPort);
        }
    }

}
