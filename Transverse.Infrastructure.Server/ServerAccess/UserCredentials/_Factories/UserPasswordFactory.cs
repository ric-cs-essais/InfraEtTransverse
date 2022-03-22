

namespace Transverse.Infrastructure.Server_
{
    public class UserPasswordFactory
    {
        private static UserPasswordFactory _oSingletonFactory = null;

        public static UserPasswordFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new UserPasswordFactory();
            }
            return _oSingletonFactory;
        }
        public UserPassword getUserPassword(string psUserPassword)
        {
            return new UserPassword(psUserPassword);
        }
    }

}
