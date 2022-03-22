
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class SqlQuery: ISqlQuery
    {
        public string AsRawString { get; }

        public List<ISqlQueryParameter<int>> IntParametersList { get; init; } = new List<ISqlQueryParameter<int>>();
        public List<ISqlQueryParameter<double>> DoubleParametersList { get; init; } = new List<ISqlQueryParameter<double>>();
        public List<ISqlQueryParameter<string>> StringParametersList { get; init; } = new List<ISqlQueryParameter<string>>();
        public List<ISqlQueryParameter<bool>> BoolParametersList { get; init; } = new List<ISqlQueryParameter<bool>>();
        public List<ISqlQueryParameter<DateTime>> DateTimeParametersList { get; init; } = new List<ISqlQueryParameter<DateTime>>();


        protected readonly string _sOpenBracketBefore = @"(\([ ]*)";
        protected readonly string _sOpenBracketAfter = @"([ ]*\()";
        protected readonly string _sSpaceOrOpenBracket = @"([ ]|\()";

        public SqlQuery(string psSqlQuery)
        {
            this.AsRawString = psSqlQuery;
            this._checkQueryValidity();

        }

        private void _checkQueryValidity()
        {
            if (!this._getQueryMinimalRegExp().IsMatch(this.AsRawString))
            {
                throw new Exception(
                    $"La requête suivante na pas été identifiée comme étant une requête SQL {this._getQueryTypeInfo()}:\n'{this.AsRawString}'"
                );
            }
        }

        protected virtual Regex _getQueryMinimalRegExp()
        {
            return new Regex(@"^*");

        }

        protected virtual string _getQueryTypeInfo()
        {
            return "";

        }
        

        public ISqlQuery setParameter(ISqlQueryParameter<int> poParameter)
        {
            this.IntParametersList.Add(poParameter);
            return (this);
        }

        public ISqlQuery setParameter(ISqlQueryParameter<double> poParameter)
        {
            this.DoubleParametersList.Add(poParameter);
            return (this);
        }

        public ISqlQuery setParameter(ISqlQueryParameter<string> poParameter)
        {
            this.StringParametersList.Add(poParameter);
            return (this);
        }

        public ISqlQuery setParameter(ISqlQueryParameter<bool> poParameter)
        {
            this.BoolParametersList.Add(poParameter);
            return (this);
        }

        public ISqlQuery setParameter(ISqlQueryParameter<DateTime> poParameter)
        {
            this.DateTimeParametersList.Add(poParameter);
            return (this);
        }

    }
}
