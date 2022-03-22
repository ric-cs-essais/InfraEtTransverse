using System;

using Transverse.Infrastructure;


namespace Transverse.Infrastructure.Server_
{
    public class Server: GenericServer<Url>
    {
        public Server(Url poServerUrl, Port poServerPort = null): base(poServerUrl, poServerPort)
        {

        }

    }
}
