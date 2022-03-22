using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class DateTimeSqlQueryParameter : ASqlQueryParameter<DateTime>
    {
        public DateTimeSqlQueryParameter(string psKey) : base(psKey) { }
        public DateTimeSqlQueryParameter(string psKey, DateTime pValue) : base(psKey, pValue) { }

        public override FieldType getType()
        {
            return FieldType.dateTime;
        }
    }
}
