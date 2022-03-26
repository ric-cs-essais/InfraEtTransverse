using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using System.Data.Common;

using Transverse.Infrastructure; //Port
using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;

using Transverse.Infrastructure.Persistence.DB.Server.Handler.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerHandler: IDBServerHandler<MySqlCommand, MySqlParameter, MySqlDbType>
    {
        private MySqlServerSqlSyntaxer _oMySqlServerSqlSyntaxer;

        public MySqlServerHandler(MySqlServerSqlSyntaxer poMySqlServerSqlSyntaxer)
        {
            this._oMySqlServerSqlSyntaxer = poMySqlServerSqlSyntaxer;

        }

        public IDBServerSqlSyntaxer getDBServerSqlSyntaxer()
        {
            return (this._oMySqlServerSqlSyntaxer);
        }

        public DbConnection createConnection(string psConnectionString)
        {
            return new MySqlConnection(psConnectionString);
        }

        public MySqlCommand createSqlQueryCommand(string psSqlQuery, DbConnection poConnection)
        {
            return new MySqlCommand(psSqlQuery, (MySqlConnection)poConnection);
        }

        public MySqlParameter createSqlQueryParameter(string psParameterFullId, MySqlDbType pFieldDbType)
        {
            return new MySqlParameter(psParameterFullId, pFieldDbType);
        }

        public Dictionary<string, string> getConnectionStringKeyValues(DBServerAccess poDBServerAccess)
        {
            Dictionary<string, string> oConnectionStringKeyValues = new Dictionary<string, string>();

            
            Port oServerPort = poDBServerAccess.Server.Port;
            if (oServerPort == null)
            {
                throw new Exception("La connexion au serveur MySQL, nécessite la mention d'un numéro de port.");
            }

            oConnectionStringKeyValues.Add("server", poDBServerAccess.Server.Url.Value);
            oConnectionStringKeyValues.Add("port", $"{oServerPort.Value}");
            oConnectionStringKeyValues.Add("uid", poDBServerAccess.UserCredentials.Name.Value);
            oConnectionStringKeyValues.Add("pwd", poDBServerAccess.UserCredentials.Password.Value);
            oConnectionStringKeyValues.Add("database", poDBServerAccess.CurrentDatabaseName?.Value ?? null);

            return (oConnectionStringKeyValues);

        }

        public string adjustInsertQueryForInstertId(string psSqlInsertQuery, string psAutoIncrementFieldName)
        {
            return psSqlInsertQuery;
        }

        public int? executeInsertAndGetLastInsertedId(MySqlCommand poSqlCommand)
        {
            poSqlCommand.ExecuteNonQuery();
            int? iInsertedId = (int?)poSqlCommand.LastInsertedId;
            return (iInsertedId);
        }


        //================= TYPES SPECIFIQUES retenus (MySqlDbType.x) ========================
        public MySqlDbType getCharDbType()
        {
            return MySqlDbType.VarChar;
        }
        public MySqlDbType getIntDbType()
        {
            return MySqlDbType.Int32;
        }
        public MySqlDbType getLongIntDbType()
        {
            return MySqlDbType.Int64;
        }
        public MySqlDbType getDoubleDbType()
        {
            return MySqlDbType.Double;
        }
        public MySqlDbType getBoolDbType()
        {
            return MySqlDbType.Bit;
        }
        public MySqlDbType getDateTimeDbType()
        {
            return MySqlDbType.DateTime;
        }

    }

}
