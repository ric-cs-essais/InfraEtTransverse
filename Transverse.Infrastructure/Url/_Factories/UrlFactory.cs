

namespace Transverse.Infrastructure
{
    public class UrlFactory
    {
        private static UrlFactory _oSingletonFactory = null;

        public static UrlFactory getSingleton()
        {
            if (_oSingletonFactory == null)
            {
                _oSingletonFactory = new UrlFactory();
            }
            return _oSingletonFactory;
        }
        public Url getUrl(string psUrl)
        {
            return new Url(psUrl);
        }
    }

}
