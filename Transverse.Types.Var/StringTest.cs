
using Transverse.Types.StringExtension;


namespace Transverse.Types.Var
{
    public class StringTest
    {
        public static bool isNotNullNorEmpty(string psString)
        {
            return (psString!=null && !psString.isEmpty());
        }

    }
}
