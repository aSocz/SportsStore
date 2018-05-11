using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Business.Validation
{
    public class ValidationResult
    {
        public ValidationResult(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; }

        public bool IsValid() => !Errors.Any();
    }
}