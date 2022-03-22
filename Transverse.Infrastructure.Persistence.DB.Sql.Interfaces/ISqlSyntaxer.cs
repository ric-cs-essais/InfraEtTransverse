
namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlSyntaxer
    {
        //================= SYNTAXES SQL SPECIFIQUES ========================
        string getSyntaxTableName(string psTableName);
        string getSyntaxRealNumberField(string psFieldName, bool pbAuthorizeNull = true);
        string getSyntaxAutoIncrementField(string psFieldName, ulong piStartValueDummy = 1, string psFieldType = "INT");
        string getSyntaxForceAutoIncrementFieldValue(string psTableName, ulong piNewValue);
        string getSyntaxCreateTable(string psTableName, string psCreateTableDescriptionData);

    }
}
