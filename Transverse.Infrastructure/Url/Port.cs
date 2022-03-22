

using System;

namespace Transverse.Infrastructure
{
    public class Port
    {
        public uint Value { get; }

        public Port(uint piValue)
        {
            if (piValue > 0)
            {
                this.Value = piValue;

            }
            else
            {
                throw new Exception("Le numéro de port doit être > 0.");
            }

        }

    }
}
