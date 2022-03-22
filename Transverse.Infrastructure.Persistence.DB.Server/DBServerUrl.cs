using Transverse.Infrastructure;


namespace Transverse.Infrastructure.Persistence.DB.Server_
{
    public class DBServerUrl: Url
    {
        public DBServerUrl(string psValue): base(psValue)
        {
            this._oPortPrefixesList.Add(",");
        }
    }
}
