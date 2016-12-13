using System.ComponentModel.DataAnnotations;

namespace TrustSystems3.ClientWeb.Utils
{
    public class BooleanRequiredAttribute : RequiredAttribute 
    {
        public override bool IsValid(object value)
        {
            return value != null && (bool) value;
        }
    }
}