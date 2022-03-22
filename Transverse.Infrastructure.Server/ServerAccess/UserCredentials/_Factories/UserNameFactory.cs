

namespace Transverse.Infrastructure.Server_
{
    public class UserNameFactory
    {
        private static UserNameFactory _oSingletonFactory = null;

        public static UserNameFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new UserNameFactory();
            }
            return _oSingletonFactory;
        }
        public UserName getUserName(string psUserName)
        {
            return new UserName(psUserName);
        }
    }

}
