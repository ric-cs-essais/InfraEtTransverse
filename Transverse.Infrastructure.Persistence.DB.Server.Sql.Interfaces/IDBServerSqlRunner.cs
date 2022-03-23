
using System.Collections.Generic;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;
using Transverse.Infrastructure.Persistence.DB.Server.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces
{
    public interface IDBServerSqlRunner
    {
        IDBServerAccess getDBServerAccess();

        IDBServerSqlSyntaxer getDBServerSqlSyntaxer();

        void execSqlFile(ISqlFileQuery poSqlFileQuery);
        void execQuery(ISqlQuery poSqlQuery);

        int? getInsertQueryResult(ISqlInsertQuery poSqlInsertQuery);
        int getUpdateQueryResult(ISqlUpdateQuery poSqlUpdateQuery);
        int getDeleteQueryResult(ISqlDeleteQuery poSqlDeleteQuery);

        List<TResultRecordType> getSelectQueryResults<TResultRecordType>(ISqlSelectQuery<TResultRecordType> poSelectQuery);
    }
}
