

namespace Transverse.Infrastructure.Persistence.DB.Database
{
    public class DatabaseNameFactory
    {
        private static DatabaseNameFactory _oSingletonFactory = null;

        public static DatabaseNameFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new DatabaseNameFactory();
            }
            return _oSingletonFactory;
        }
        public DatabaseName getDatabaseName(string psDatabaseName)
        {
            return new DatabaseName(psDatabaseName);
        }
    }

}
