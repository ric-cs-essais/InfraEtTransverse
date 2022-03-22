using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class BoolSqlQueryParameter : ASqlQueryParameter<bool>
    {
        public BoolSqlQueryParameter(string psKey) : base(psKey) { }
        public BoolSqlQueryParameter(string psKey, bool pValue) : base(psKey, pValue) { }

        public override FieldType getType()
        {
            return FieldType.boolean;
        }
    }
}
