using System.Collections.Generic;

using System.Data.Common;

using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Handler.Interfaces
{
    public interface IDBServerHandler<TDbCommand, TDbParameter, TDbType>
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
    {
        IDBServerSqlSyntaxer getDBServerSqlSyntaxer();

        DbConnection createConnection(string psConnectionString);

        TDbCommand createSqlQueryCommand(string psSqlQuery, DbConnection poConnection);

        TDbParameter createSqlQueryParameter(string psParameterFullId, TDbType pFieldDbType);

        Dictionary<string, string> getConnectionStringKeyValues(DBServerAccess poDBServerAccess);

        string adjustInsertQueryForInstertId(string psSqlInsertQuery, string psAutoIncrementFieldName);

        int? executeInsertAndGetLastInsertedId(TDbCommand poSqlCommand);


        //================= TYPES SPECIFIQUES retenus (TDbType.x) ========================
        TDbType getCharDbType();
        TDbType getIntDbType();
        TDbType getLongIntDbType();
        TDbType getDoubleDbType();
        TDbType getBoolDbType();
        TDbType getDateTimeDbType();

    }
}