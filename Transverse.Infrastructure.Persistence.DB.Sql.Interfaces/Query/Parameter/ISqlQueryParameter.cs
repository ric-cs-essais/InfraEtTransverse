using Transverse.Infrastructure.Persistence.DB.Sql.Enums;


namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{ 
    public interface ISqlQueryParameter<TValueType>
    {
        string Key { get; }
        TValueType Value { get; set; }

        FieldType getType();
    }

}
