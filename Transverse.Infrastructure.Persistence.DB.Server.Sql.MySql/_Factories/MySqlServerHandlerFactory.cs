

namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerHandlerFactory
    {
        private static MySqlServerHandlerFactory _oSingletonFactory = null;

        private MySqlServerHandler _oMySqlServerHandlerSingleton = null;

        public static MySqlServerHandlerFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new MySqlServerHandlerFactory();
            }
            return _oSingletonFactory;
        }

        public MySqlServerHandler getMySqlServerHandlerSingleton()
        {
            if (this._oMySqlServerHandlerSingleton == null)
            {
                this._oMySqlServerHandlerSingleton = new MySqlServerHandler(
                    this._getMySqlServerSqlSyntaxer()
                );
            }
            return this._oMySqlServerHandlerSingleton;
        }

        private MySqlServerSqlSyntaxer _getMySqlServerSqlSyntaxer()
        {
            return MySqlServerSqlSyntaxerFactory.getSingleton().getMySqlServerSqlSyntaxerSingleton();
        }

    }

}
