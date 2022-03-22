

using System;

using Transverse.Types.Var;


namespace Transverse.Infrastructure.Persistence.DB.Database
{
    public class DatabaseName
    {
        public string Value { get; }

        public DatabaseName(string psValue)
        {
            if (StringTest.isNotNullNorEmpty(psValue))
            {
                this.Value = psValue;

            }
            else
            {
                throw new Exception("Le Database Name ne peut valoir null, ni être vide.");
            }

        }

    }
}
