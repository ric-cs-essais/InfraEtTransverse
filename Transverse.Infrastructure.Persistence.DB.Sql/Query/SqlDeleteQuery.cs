using System.Text.RegularExpressions;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlDeleteQuery : SqlQuery, ISqlDeleteQuery
    {
        public SqlDeleteQuery(string psSqlQuery) : base(psSqlQuery)
        {
        }

        protected override Regex _getQueryMinimalRegExp()
        {
            return new Regex(
                $"^[ ]*DELETE[ ]+FROM{this._sSpaceOrOpenBracket}+"
                , RegexOptions.IgnoreCase
            );

        }

        protected override string _getQueryTypeInfo()
        {
            return "de type 'DELETE' ";

        }
    }
}
