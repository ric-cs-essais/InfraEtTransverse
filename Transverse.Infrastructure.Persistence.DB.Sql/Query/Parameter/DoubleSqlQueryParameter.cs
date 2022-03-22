
using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class DoubleSqlQueryParameter : ASqlQueryParameter<double>
    {
        public DoubleSqlQueryParameter(string psKey) : base(psKey) { }
        public DoubleSqlQueryParameter(string psKey, double pValue) : base(psKey, pValue) { }

        public override FieldType getType()
        {
            return FieldType.dbl;
        }
    }
}
