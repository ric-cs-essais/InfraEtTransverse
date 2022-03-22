

namespace Transverse.Infrastructure.Server_
{
    public abstract class AGenericServerAccessFactory<TServerAccess>
    {
        public abstract TServerAccess getServerAccess(string psUrl, string psUserName, string psUserPassword, uint? piPort = null);

        protected UserCredentials _getUserCredentials(string psUserName, string psUserPassword)
        {
            return UserCredentialsFactory.getSingleton().getUserCredentials(psUserName, psUserPassword);
        }
    }

}
