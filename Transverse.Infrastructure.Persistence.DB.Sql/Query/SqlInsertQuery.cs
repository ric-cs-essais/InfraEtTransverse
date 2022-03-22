using System.Text.RegularExpressions;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlInsertQuery : SqlQuery, ISqlInsertQuery
    {
        public string AutoIncrementFieldName { get; init; } = null;

        public SqlInsertQuery(string psSqlQuery) : base(psSqlQuery)
        {
        }

        protected override Regex _getQueryMinimalRegExp()
        {
            return new Regex(
                $"^[ ]*INSERT[ ]+INTO[ ]+.*[ ]+VALUES{this._sOpenBracketAfter}.*"
                , RegexOptions.IgnoreCase
            );

        }

        protected override string _getQueryTypeInfo()
        {
            return "de type 'INSERT' ";

        }
    }
}
