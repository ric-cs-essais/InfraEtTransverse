using System;

using Transverse.Infrastructure;


namespace Transverse.Infrastructure.Server_
{
    public class GenericServerAccess<TServer>
    {
        public TServer Server { get; }
        public UserCredentials UserCredentials { get; }

        public GenericServerAccess(TServer poServer, UserCredentials poUserCredentials)
        {
            this.Server = poServer;
            this.UserCredentials = poUserCredentials;

        }

    }
}
