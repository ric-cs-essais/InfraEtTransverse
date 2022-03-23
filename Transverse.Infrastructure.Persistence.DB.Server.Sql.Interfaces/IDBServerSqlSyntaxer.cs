
namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces
{
    public interface IDBServerSqlSyntaxer
    {
        //================= SYNTAXES SQL SPECIFIQUES à un DB Server ========================
        string getSyntaxTableName(string psTableName);
        string getSyntaxRealNumberField(string psFieldName, bool pbAuthorizeNull = true);
        string getSyntaxAutoIncrementField(string psFieldName, ulong piStartValueDummy = 1, string psFieldType = "INT");
        string getSyntaxForceAutoIncrementFieldValue(string psTableName, ulong piNewValue);
        string getSyntaxCreateTable(string psTableName, string psCreateTableDescriptionData);

    }
}
