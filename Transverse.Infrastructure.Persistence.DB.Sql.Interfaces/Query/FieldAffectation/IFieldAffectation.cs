using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface IFieldAffectation<TRecordType>
    {
        FieldType FieldType { get; set; }
        Action<object, TRecordType> fFieldAffectation { get; set; } //Type : Function sans val. de retour, et recevant en param. un type object (c-à-d n'importe quel type, même basique) représentant la valeur à affecter, et un type TRecordType, représentant l'objet dont le champ est à affecter.

    }
}
