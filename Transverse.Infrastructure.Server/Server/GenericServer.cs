using System;

using Transverse.Infrastructure;


namespace Transverse.Infrastructure.Server_
{
    public class GenericServer<TUrl>
        where TUrl: Url
    {
        public TUrl Url { get; }
        public Port Port { get; private set; }

        public GenericServer(TUrl poServerUrl, Port poServerPort = null)
        {
            this.Url = poServerUrl;
            this.Port = poServerPort;

            this._checkConsistency();
            this._affectPortIfPossibleAndNotSpecified();
        }


        private void _checkConsistency()
        {
            this._portMustBeSpecifiedOnlyOnce();
        }

        private void _portMustBeSpecifiedOnlyOnce()
        {
            if (this.Port != null)
            {
                uint? iUrlPort;
                if ((iUrlPort = this.Url.getPortAsInt()) != null)
                {
                    throw new Exception($"Le port doit être spécifié ou bien dans l'URL, ou bien indépendamment (port dans l'URL={iUrlPort} vs {this.Port.Value}).");
                }
            }
        }

        private void _affectPortIfPossibleAndNotSpecified()
        {
            if (this.Port == null)
            {
                this.Port = this.Url.getPort();
            }
        }
    }
}
