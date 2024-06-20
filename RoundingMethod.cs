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
            // TODO: Implement your custom configuration handling
            // https://docs.microsoft.com/powerapps/developer/common-data-service/register-plug-in#set-configuration-data
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            decimal numberInput = context.InputParameters["roboest_decimalinput"];
            bool forcePosititve = If(
                context.InputParameters.contains("roboest_forceposititve"),
                context.InputParameters["roboest_forceposititve"],
                false
            );
            String chosenMethod =
                context.InputParameters["roboest_roundingmethod"].label;

            
            int result = Rounder.RoundNumber(numberInput, forcePosititve, chosenMethod);

            context.OutputParameters["roboest_integerResult"] = result;

            // TODO: Implement your custom business logic

            // Check for the entity on which the plugin would be registered
            //if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            //{
            //    var entity = (Entity)context.InputParameters["Target"];

            //    // Check for entity name on which this plugin would be registered
            //    if (entity.LogicalName == "account")
            //    {

            //    }
            //}
        }
    }
}
