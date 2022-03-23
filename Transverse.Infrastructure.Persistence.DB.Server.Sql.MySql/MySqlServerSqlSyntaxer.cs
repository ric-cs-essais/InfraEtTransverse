
using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql
{
    public class MySqlServerSqlSyntaxer: IDBServerSqlSyntaxer
    {
        public string getSyntaxTableName(string psTableName)
        {
            return $"`{psTableName}`";
        }
        public string getSyntaxRealNumberField(string psFieldName, bool pbAuthorizeNull = true)
        {
            string sNullOrNot = (pbAuthorizeNull) ? "" : "NOT ";
            return $"{psFieldName} DOUBLE {sNullOrNot}NULL,"; //"DOUBLE", le mieux que j'ai pu constater pour MySQL
        }
        public string getSyntaxAutoIncrementField(string psFieldName, ulong piStartValueDummy = 0, string psFieldType = "INT")
        {
            return $"{psFieldName} {psFieldType} NOT NULL AUTO_INCREMENT,";
        }
        public string getSyntaxForceAutoIncrementFieldValue(string psTableName, ulong piNewValue)
        {
            return $"ALTER TABLE {this.getSyntaxTableName(psTableName)} AUTO_INCREMENT = {piNewValue};";
        }
        public string getSyntaxCreateTable(string psTableName, string psCreateTableDescriptionData)
        {
            return $"CREATE TABLE IF NOT EXISTS {this.getSyntaxTableName(psTableName)} (\n{psCreateTableDescriptionData}\n);";
        }

    }
}
