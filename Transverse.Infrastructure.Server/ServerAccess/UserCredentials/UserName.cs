using System;

using Transverse.Types.Var;


namespace Transverse.Infrastructure.Server_
{
    public class UserName
    {
        public string Value { get; }

        public UserName(string psValue)
        {
            if (StringTest.isNotNullNorEmpty(psValue))
            {
                this.Value = psValue;

            } 
            else
            {
                throw new Exception("Le User Name ne peut valoir null, ni être vide.");
            }

        }

    }
}
