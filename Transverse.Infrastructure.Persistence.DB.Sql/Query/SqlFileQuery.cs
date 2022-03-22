
using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlFileQuery : SqlQuery, ISqlFileQuery
    {
        public string SqlScriptFile { get; }

        public SqlFileQuery(string psSqlScriptFile) : base("")
        {
            this.SqlScriptFile = psSqlScriptFile;
        }

        protected override string _getQueryTypeInfo()
        {
            return $"du fichier '{this.SqlScriptFile}' ";

        }
    }
}
