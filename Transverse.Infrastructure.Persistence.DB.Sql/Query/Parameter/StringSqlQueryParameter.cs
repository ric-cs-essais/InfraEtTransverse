using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class StringSqlQueryParameter : ASqlQueryParameter<string>
    {
        public StringSqlQueryParameter(string psKey) : base(psKey) { }
        public StringSqlQueryParameter(string psKey, string pValue) : base(psKey, pValue) { }

        public override FieldType getType()
        {
            return FieldType.str;
        }
    }
}
