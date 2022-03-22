using System;

using Transverse.Types.Var;


namespace Transverse.Infrastructure.Server_
{
    public class UserPassword
    {
        public string Value { get; }

        public UserPassword(string psValue)
        {
            if (StringTest.isNotNullNorEmpty(psValue))
            {
                this.Value = psValue;

            }
            else
            {
                throw new Exception("Le User Password ne peut valoir null, ni être vide.");
            }

        }

    }
}
