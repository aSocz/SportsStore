using SportsStore.Domain.Interfaces;

namespace SportsStore.Business.Validation
{
    public interface IValidator<in T> where T : IValidatable
    {
        ValidationResult Validate(T validatableObject);
    }
}