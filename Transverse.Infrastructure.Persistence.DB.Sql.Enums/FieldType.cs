
namespace Transverse.Infrastructure.Persistence.DB.Sql.Enums
{
    public enum FieldType : ushort
    {
        str = 1, //string
        integer = 2, //Int32 (-2 147 483 648   à   +2 147 483 647)
        longInteger = 3, //Int64
        boolean = 4,
        dateTime = 5,
        dbl = 6 //double (éviter float, incompatibilité avec le type "float" de MySQL par exemple).

    }
}
