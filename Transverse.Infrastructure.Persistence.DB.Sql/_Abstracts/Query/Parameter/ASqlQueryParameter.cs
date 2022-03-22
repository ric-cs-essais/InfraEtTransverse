
using System;

using Transverse.Infrastructure.Persistence.DB.Sql.Enums;
using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Sql
{
    public abstract class ASqlQueryParameter<TValueType>: ISqlQueryParameter<TValueType>
    {
        public TValueType Value { get; set; }
        public string Key { get; }

        public abstract FieldType getType();

        //Remarque : pas de sensibilité à la casse pour la clef.
        protected ASqlQueryParameter(string psKey)
        {
            this.Key = psKey;
        }

        protected ASqlQueryParameter(string psKey, TValueType pValue): this(psKey)
        {
            if (pValue != null)
            {
                this.Value = pValue;
            }
            else throw new Exception($"Tous les paramètres dont la clef (ici @{psKey}) stipulés dans le requête SQL doivent être renseignés (ET non null).");
        }

    }
}
