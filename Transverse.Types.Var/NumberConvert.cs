using System;


namespace Transverse.Types.Var
{
    public class NumberConvert
    {
        public static string toString(object pNumber)
        {
            string sRetour;

            if (!NumberTest.isNumber(pNumber))
            {
                throw new Exception($"pNumber doit être un nombre, pNumber={pNumber}");
            }

            sRetour = $"{pNumber}";

            if (NumberTest.isFloatOrDouble(pNumber))
            {
                sRetour = sRetour.Replace(",", ".");
            }

            return (sRetour);
        }

    }
}
