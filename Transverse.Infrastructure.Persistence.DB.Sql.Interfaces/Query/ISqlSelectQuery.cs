using System;
using System.Collections.Generic;



namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlSelectQuery<TResultRecordType> : ISqlQuery
    {
        public Func<TResultRecordType> fCreateResultNewRecord { get; init; }
        public IReadOnlyCollection<IFieldAffectation<TResultRecordType>> FieldsAffectionOrderedList { get; init; }

    }
}
