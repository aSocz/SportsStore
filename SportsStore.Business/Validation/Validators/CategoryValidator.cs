using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Business.Validation.Validators
{
    public class CategoryValidator : IValidator<Category>
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryValidator(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ValidationResult Validate(Category validatableObject)
        {
            var validationErrors = new List<ValidationError>();
            var doesAnyCategoryWithSameNameExist = categoryRepository
                                                  .GetNoTracking(
                                                       c => c.Name.Equals(
                                                           validatableObject.Name,
                                                           StringComparison.Ordinal))
                                                  .Any();

            if (doesAnyCategoryWithSameNameExist)
            {
                validationErrors.Add(new ValidationError("Kategoria z podaną nazwą już istnieje", nameof(Category.Name)));
            }

            return new ValidationResult(validationErrors);
        }
    }
}