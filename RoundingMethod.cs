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
            : base(typeof(RoundingMethod)) { }

        // Round the incoming decimal if all the requirements for a valid request are met
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            // Process all the input parameters
            decimal numberInput = (decimal)context.InputParameters["roboest_decimalinput"]; //Required
            bool forcePosititve = (bool)context.InputParameters["roboest_forceposititve"]; //Optional and default value false
            string chosenMethod = context.InputParameters["roboest_roundingmethod"] != null
                ? (string)context.InputParameters["roboest_roundingmethod"]
                : "Round"; //Optional and default value "Round"

            localPluginContext.Trace($"Processed received parameters as numberInput {numberInput}, boolean forcePosititve {forcePosititve}, string chosenMethod {chosenMethod}");

            // Check validity of the chosenMethod string
            if (!Functionality.isValidRoundingMethod(chosenMethod))
            {
                localPluginContext.Trace($"Confirmed that the chosenMethod was not a valid option with the input of: {chosenMethod}");
                throw new ArgumentException(
                    "Rounding Method",
                    "The options for rounding are limited to: Round, RoundUp, RoundDown"
                );
            }

            // Return the rounded Integer if no errors occur
            context.OutputParameters["roboest_integerResult"] = Functionality.RoundNumber(
                numberInput,
                forcePosititve,
                chosenMethod
            );
        }
    }
}
