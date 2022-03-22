using System.Text.RegularExpressions;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlUpdateQuery : SqlQuery, ISqlUpdateQuery
    {
        public SqlUpdateQuery(string psSqlQuery) : base(psSqlQuery)
        {
        }

        protected override Regex _getQueryMinimalRegExp()
        {
            return new Regex(
                $"^[ ]*UPDATE[ ]+.*[ ]+SET[ ]+"
                , RegexOptions.IgnoreCase
            );

        }

        protected override string _getQueryTypeInfo()
        {
            return "de type 'UPDATE' ";

        }
    }
}
