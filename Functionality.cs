using System;
using System.Collections;
using System.Diagnostics;
using System.ServiceModel.Security;

namespace Rounding
{
    public static class Functionality
    {

        public static bool isValidRoundingMethod(string roundingMethod) {
            string cleanedString = Regex.Replace(input, "[^a-zA-Z]", string.Empty);

            return (cleanedString == "Round" || cleanedString == "RoundUp" || cleanedString == "RoundDown");
        }

        public static int RoundNumber(decimal number, bool forcePosititve, string roundingMethod)
        {
            int result = 0;

            if (number < 0 && forcePosititve)
            {
                // If the number is smaller than 0 and this isn't allowed the rounding will always be 0
                result = 0;
            }
            else
            {
                //complete the requested rounding the default situation being a normal Round
                switch (roundingMethod)
                {
                    case "RoundUp":
                        result = (int)Math.Ceiling(number);
                        break;
                    case "RoundDown":
                        result = (int)Math.Floor(number);
                        break;
                    default:
                        result = (int)Math.Round(number);
                        break;
                }
            }

            return result;
        }
    }
}
