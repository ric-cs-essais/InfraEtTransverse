

namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlInsertQuery : ISqlQuery
    {
        string AutoIncrementFieldName { get; init; }

    }
}
