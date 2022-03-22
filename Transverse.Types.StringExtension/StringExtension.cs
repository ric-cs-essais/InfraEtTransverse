

namespace Transverse.Types.StringExtension
{
    public static class StringExtension
    {
        public static string backSlash(this string psString, bool pbBackSlash = true)
        {
            string sBackSlash = @"\";
            string sSlash = "/";
            string sToBeReplaced = (pbBackSlash) ? sSlash : sBackSlash;
            string sReplaceBy = (pbBackSlash) ? sBackSlash : sSlash;

            string sString = psString.Replace(sToBeReplaced, sReplaceBy);
            return (sString);
        }

        public static string endsWith(this string psString, string psEnd, bool pbMustEndWith)
        {
            string sString = psString;

            if (!psString.EndsWith(psEnd) && pbMustEndWith)
            {
                sString += psEnd;

            } else if (psString.EndsWith(psEnd) && !pbMustEndWith)
            {
                sString = sString.Substring(0, sString.Length - psEnd.Length);
            }

            return (sString);
        }

        public static bool isEmpty(this string psString)
        {
            bool bReponse = (psString.Trim()=="");
            return (bReponse);
        }

    }
}
