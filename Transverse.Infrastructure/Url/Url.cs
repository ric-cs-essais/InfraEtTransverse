using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Transverse.Infrastructure
{
    public class Url
    {
        public string Value { get; }

        protected List<string> _oPortPrefixesList = new List<string> { ":" };

        public Url(string psValue)
        {
            if (psValue != null)
            {
                this.Value = psValue;

            }
            else
            {
                throw new Exception("L'URL ne peut valoir null.");
            }

        }


        public Port getPort()
        {
            uint? iPort = this.getPortAsInt();

            Port oPort = (iPort != null) ? new Port((uint)iPort) : null;
            return (oPort);
        }

        public uint? getPortAsInt()
        {
            string sPort = this._getPortAsString();

            uint? iPort = (sPort != null) ? UInt32.Parse(sPort) : null;
            return (iPort);
        }

        private string _getPortAsString()
        {
            Regex oFindPortInUrlRegExp = this._getFindPortInUrlRegExp();
            Match oMatch = oFindPortInUrlRegExp.Match(this.Value);

            string sPort = (oMatch.Success) ? oMatch.Groups[2].Value : null;
            return (sPort);

        }

        private Regex _getFindPortInUrlRegExp()
        {
            string sPortPrefixes = this._getPortPrefixesToFindPortInUrlRegExp();
            string sFindPortInUrlRegExp = $"[A-Z]+({sPortPrefixes})" + "([0-9]{2,})";

            Regex oFindPortInUrlRegExp = new Regex(sFindPortInUrlRegExp, RegexOptions.IgnoreCase);
            return (oFindPortInUrlRegExp);
        }

        private string _getPortPrefixesToFindPortInUrlRegExp()
        {
            string sPortPrefixes = string.Join("|", this._oPortPrefixesList);
            return (sPortPrefixes);
        }
    }
}
