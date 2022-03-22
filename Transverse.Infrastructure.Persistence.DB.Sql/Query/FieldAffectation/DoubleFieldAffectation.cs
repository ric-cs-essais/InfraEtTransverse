using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class DoubleFieldAffectation<TRecordType> : FieldAffectation<TRecordType>
    {
        public DoubleFieldAffectation(Action<object, TRecordType> pfFieldAffectation) : base(pfFieldAffectation)
        {
            this.FieldType = FieldType.dbl;

        }
    }
}
