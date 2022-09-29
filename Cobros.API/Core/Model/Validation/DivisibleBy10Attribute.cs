using System.ComponentModel.DataAnnotations;

namespace Cobros.API.Core.Model.Validation
{
    public class DivisibleBy10Attribute : ValidationAttribute
    {
        public DivisibleBy10Attribute() : base("{0} is not valid.")
        {}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int val = (int) value;

            if(val % 10 != 0)
                return new ValidationResult($"{val} is not divisible by 10");

            return null;
        }
    }
}
