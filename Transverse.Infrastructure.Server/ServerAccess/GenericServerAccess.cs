using System;

using Transverse.Infrastructure;


namespace Transverse.Infrastructure.Server_
{
    public class GenericServerAccess<TServer>
    {
        public TServer Server { get; init; }
        public UserCredentials UserCredentials { get; init; }

        public GenericServerAccess(TServer poServer, UserCredentials poUserCredentials)
        {
            this.Server = poServer;
            this.UserCredentials = poUserCredentials;

        }

    }
}
