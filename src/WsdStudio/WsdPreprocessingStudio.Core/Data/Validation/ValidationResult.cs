using System.Collections.Generic;
using System.Linq;

namespace WsdPreprocessingStudio.Core.Data.Validation
{
    public class ValidationResult
    {
        public static readonly ValidationResult Success = null;

        public IReadOnlyCollection<ValidationError> Errors { get; }

        public ValidationResult(IList<ValidationError> errors)
        {
            Errors = (errors ?? new ValidationError[0]).ToList().AsReadOnly();
        }
    }
}
