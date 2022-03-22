using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class LongIntFieldAffectation<TRecordType> : FieldAffectation<TRecordType>
    {
        public LongIntFieldAffectation(Action<object, TRecordType> pfFieldAffectation) : base(pfFieldAffectation)
        {
            this.FieldType = FieldType.longInteger;

        }
    }
}
