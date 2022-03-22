
using System.Collections.Generic;



namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlRunner
    {
        ISqlSyntaxer getSyntaxer();

        //void setCurrentDatabaseName(DatabaseName poCurrentDatabaseName);

        void execSqlFile(ISqlFileQuery poSqlFileQuery);
        void execQuery(ISqlQuery poSqlQuery);

        int? getInsertQueryResult(ISqlInsertQuery poSqlInsertQuery);
        int getUpdateQueryResult(ISqlUpdateQuery poSqlUpdateQuery);
        int getDeleteQueryResult(ISqlDeleteQuery poSqlDeleteQuery);

        List<TResultRecordType> getSelectQueryResults<TResultRecordType>(ISqlSelectQuery<TResultRecordType> poSelectQuery);
    }
}
