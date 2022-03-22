namespace Transverse.Infrastructure { 
    public class PortFactory
    {
        private static PortFactory _oSingletonFactory = null;

        public static PortFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new PortFactory();
            }
            return _oSingletonFactory;
        }
        public Port getPort(uint piPort)
        {
            return new Port(piPort);
        }
    }

}