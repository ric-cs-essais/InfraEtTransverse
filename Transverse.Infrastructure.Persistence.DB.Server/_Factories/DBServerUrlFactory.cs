

namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerUrlFactory
    {
        private static DBServerUrlFactory _oSingletonFactory = null;

        public static DBServerUrlFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new DBServerUrlFactory();
            }
            return _oSingletonFactory;
        }
        public DBServerUrl getDBServerUrl(string psDBServerUrl)
        {
            return new DBServerUrl(psDBServerUrl);
        }
    }

}
