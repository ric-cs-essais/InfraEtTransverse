

namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerSqlSyntaxerFactory
    {
        private static MySqlServerSqlSyntaxerFactory _oSingletonFactory = null;

        private MySqlServerSqlSyntaxer _oMySqlServerSqlSyntaxerSingleton = null;

        public static MySqlServerSqlSyntaxerFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new MySqlServerSqlSyntaxerFactory();
            }
            return _oSingletonFactory;
        }

        public MySqlServerSqlSyntaxer getMySqlServerSqlSyntaxerSingleton()
        {
            if (this._oMySqlServerSqlSyntaxerSingleton == null)
            {
                this._oMySqlServerSqlSyntaxerSingleton = new MySqlServerSqlSyntaxer();
            }
            return this._oMySqlServerSqlSyntaxerSingleton;
        }

    }

}
