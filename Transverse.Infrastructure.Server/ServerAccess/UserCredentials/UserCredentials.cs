

namespace Transverse.Infrastructure.Server_
{
    public class UserCredentials
    {
        public UserName Name { get; }
        public UserPassword Password { get; }

        public UserCredentials(UserName poName, UserPassword poPassword)
        {
            this.Name = poName;
            this.Password = poPassword;
        }

    }
}
