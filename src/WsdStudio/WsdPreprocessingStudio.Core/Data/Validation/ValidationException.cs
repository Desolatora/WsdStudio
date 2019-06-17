using System;
using System.Text;

namespace WsdPreprocessingStudio.Core.Data.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationResult validationResult)
            : base(ResultToMessage(validationResult))
        {
            
        }

        public static string ResultToMessage(ValidationResult validationResult)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("One or more validation errors occured:");

            foreach (var error in validationResult.Errors)
            {
                stringBuilder.AppendLine($"{error.MemberName} - {error.ErrorMessage}");
            }

            return stringBuilder.ToString();
        }
    }
}
