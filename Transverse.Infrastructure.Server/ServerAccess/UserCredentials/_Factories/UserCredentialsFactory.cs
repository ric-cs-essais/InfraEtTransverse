

namespace Transverse.Infrastructure.Server_
{
    public class UserCredentialsFactory
    {
        private static UserCredentialsFactory _oSingletonFactory = null;

        public static UserCredentialsFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new UserCredentialsFactory();
            }
            return _oSingletonFactory;
        }

        public UserCredentials getUserCredentials(string psUserName, string psUserPassword)
        {
            return new UserCredentials(
                this._getUserName(psUserName),
                this._getUserPassword(psUserPassword)
            );
        }

        private UserName _getUserName(string psUserName)
        {
            return UserNameFactory.getSingleton().getUserName(psUserName);
        }

        private UserPassword _getUserPassword(string psUserPassword)
        {
            return UserPasswordFactory.getSingleton().getUserPassword(psUserPassword);
        }
    }

}
