using System;


namespace Transverse.Types.Var
{
    public class DateConvert
    {
        public static string toDateTimeString(DateTime? poDateTime)
        {
            return ((poDateTime == null) ? "" : ((DateTime)poDateTime).ToString("dd/MM/yyyy HH:mm:ss"));
        }

        public static string toDateString(DateTime? poDateTime)
        {
            return ((poDateTime == null) ? "" : ((DateTime)poDateTime).ToString("dd/MM/yyyy"));
        }
    }
}
