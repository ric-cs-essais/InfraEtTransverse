
namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlFileQuery : ISqlQuery
    {
        string SqlScriptFile { get; init; }

    }
}
