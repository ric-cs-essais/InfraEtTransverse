

namespace Transverse.Infrastructure.Server_
{
    public abstract class AGenericServerFactory<TServer>
    {
        public abstract TServer getServer(string psUrl, uint? piPort = null);

        protected Port _getPort(uint piPort)
        {
            return PortFactory.getSingleton().getPort(piPort);
        }
    }

}
