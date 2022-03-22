
using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class IntSqlQueryParameter : ASqlQueryParameter<int>
    {
        public IntSqlQueryParameter(string psKey) : base(psKey) { }
        public IntSqlQueryParameter(string psKey, int pValue) : base(psKey, pValue) { }

        public override FieldType getType()
        {
            return FieldType.integer;
        }
    }
}
