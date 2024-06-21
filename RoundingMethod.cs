using System;
using Microsoft.Xrm.Sdk;

namespace Rounding
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class RoundingMethod : PluginBase
    {
        public RoundingMethod(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(RoundingMethod))
        {
           
        }

        // Round the incoming decimal if all the requirements for a valid request are met
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            // Set Decimal Input as required but unbound action doesn't force it
            if(!context.InputParameters.Contains("roboest_decimalinput")){
                throw new ArgumentNullException("Decimal", "The required value of the Decimal to be converted was empty");
            }

            // Process all the input parameters
            decimal numberInput = (decimal)context.InputParameters["roboest_decimalinput"]; //Required
            bool forcePosititve = context.InputParameters.Contains("roboest_forceposititve")
                ? (bool)context.InputParameters["roboest_forceposititve"]
                : false; //Optional and default value false
            string chosenMethod = context.InputParameters.Contains("roboest_roundingmethod")
                ? (string)context.InputParameters["roboest_roundingmethod"]
                : "Round"; //Optional and default value "Round"

            // Check validity of the chosenMethod string
            if(Functionality.isValidRoundingMethod(chosenMethod)){
                throw new ArgumentException("Rounding Method", "The options for rounding are limited to: Round, RoundUp, RoundDown");
            }

            // Return the rounded Integer if no errors occur
            context.OutputParameters["roboest_integerResult"] = Functionality.RoundNumber(numberInput, forcePosititve, chosenMethod);

        }
    }
}
