
namespace Transverse.Types.Var
{
    public class NumberTest
    {
        public static bool isFloat(object pVar)
        {
            return (VarInfos.getType(pVar) == "System.Single");
        }
        public static bool isDouble(object pVar)
        {
            return (VarInfos.getType(pVar) == "System.Double");
        }
        public static bool isFloatOrDouble(object pVar)
        {
            return (isFloat(pVar) || isDouble(pVar));
        }

        public static bool isInt(object pVar)
        {
            return (VarInfos.getType(pVar) == "System.Int32");
        }
        public static bool isLongInt(object pVar)
        {
            return (VarInfos.getType(pVar) == "System.Int64");
        }
        public static bool isInteger(object pVar)
        {
            return (isInt(pVar) || isLongInt(pVar));
        }

        public static bool isNumber(object pVar)
        {
            return (isInteger(pVar) || isFloatOrDouble(pVar));
        }

    }
}
