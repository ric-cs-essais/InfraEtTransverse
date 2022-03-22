using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class BoolFieldAffectation<TRecordType>: FieldAffectation<TRecordType>
    {
        public BoolFieldAffectation(Action<object, TRecordType> pfFieldAffectation) : base(pfFieldAffectation)
        {
            this.FieldType = FieldType.boolean;
        }
    }
}
