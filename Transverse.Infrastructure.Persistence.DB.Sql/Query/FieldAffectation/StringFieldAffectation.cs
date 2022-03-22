using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public class StringFieldAffectation<TRecordType> : FieldAffectation<TRecordType>
    {
        public StringFieldAffectation(Action<object, TRecordType> pfFieldAffectation) : base(pfFieldAffectation)
        {
            this.FieldType = FieldType.str;

            //Override de la lambda chargée de l'affectation
            this.fFieldAffectation = (object pFieldValue, TRecordType poRecord) =>
            {
                pfFieldAffectation(this._adjustFieldValue(pFieldValue), poRecord);
            };
        }

        private string _adjustFieldValue(object pFieldValue)
        {
            return ((pFieldValue == null) ? null : ((string)pFieldValue).Trim());
        }
    }
}
