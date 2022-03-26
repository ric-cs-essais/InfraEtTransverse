
using System;
using Transverse.Infrastructure.Persistence.DB.Server_;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerSqlRunnerFactory
    {
        private static MySqlServerSqlRunnerFactory _oSingletonFactory = null;

        public static MySqlServerSqlRunnerFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new MySqlServerSqlRunnerFactory();
            }
            return _oSingletonFactory;
        }

        public MySqlServerSqlRunner getMySqlServerSqlRunner(DBServerAccess poDBServerAccess)
        {
            return new MySqlServerSqlRunner(
                poDBServerAccess,
                this._getMySqlServerHandler()
            );
        }

        private MySqlServerHandler _getMySqlServerHandler()
        {
            return MySqlServerHandlerFactory.getSingleton().getMySqlServerHandlerSingleton();
        }
    }

}
