using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class DateTimeFieldAffectation<TRecordType> : FieldAffectation<TRecordType>
    {
        public DateTimeFieldAffectation(Action<object, TRecordType> pfFieldAffectation) : base(pfFieldAffectation)
        {
            this.FieldType = FieldType.dateTime;
        }
    }
}
