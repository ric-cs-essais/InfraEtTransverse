using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;
using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class FieldAffectation<TRecordType>: IFieldAffectation<TRecordType>
    {
        public FieldType FieldType { get; set; }
        public Action<object, TRecordType> fFieldAffectation { get; set; }  //Type : Function sans val. de retour, et recevant en param. un type object (c-à-d n'importe quel type, même basique) représentant la valeur à affecter, et un type TRecordType, représentant l'objet dont le champ est à affecter.

        protected FieldAffectation(Action<object, TRecordType> pfFieldAffectation)
        {
            this.fFieldAffectation = pfFieldAffectation;
        }
    }
}
