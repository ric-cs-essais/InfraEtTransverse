using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlSelectQuery<TResultRecordType> : SqlQuery, ISqlSelectQuery<TResultRecordType>
    {
        public Func<TResultRecordType> fCreateResultNewRecord { get; init; }
        public IReadOnlyCollection<IFieldAffectation<TResultRecordType>> FieldsAffectionOrderedList { get; init; }

        public SqlSelectQuery(string psSqlQuery) : base(psSqlQuery)
        {
        }

        protected override Regex _getQueryMinimalRegExp()
        {
            string sRegExp = $"^[ ]*{this._sOpenBracketBefore}*SELECT{this._sSpaceOrOpenBracket}+.*{this._sSpaceOrOpenBracket}+FROM{this._sSpaceOrOpenBracket}+.*";
            //Console.WriteLine($"'{sRegExp}'");

            return new Regex(
                sRegExp
                , RegexOptions.IgnoreCase
            );

        }

        protected override string _getQueryTypeInfo()
        {
            return "de type 'SELECT' ";

        }
    }
}
