using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Business.Validation.Validators
{
    public class ProductValidator : IValidator<Product>
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;

        public ProductValidator(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public ValidationResult Validate(Product validatableObject)
        {
            var validationErrors = new List<ValidationError>();
            var doesAnyActiveProductWithTheSameNameAndCategoryExist =
                productRepository.GetNoTracking(
                                      p => p.Name.Equals(validatableObject.Name, StringComparison.Ordinal)
                                        && p.CategoryId == validatableObject.CategoryId
                                        && p.IsActive)
                                 .Any();

            if (doesAnyActiveProductWithTheSameNameAndCategoryExist)
            {
                validationErrors.Add(new ValidationError("Produkt z podaną nazwą już istnieje", nameof(Product.Name)));
            }

            var isPriceCorrect = validatableObject.Price > 0;

            if (!isPriceCorrect)
            {
                validationErrors.Add(new ValidationError("Podana cena jest niepoprawna", nameof(Product.Price)));
            }

            var isCategoryCorrect = categoryRepository
                                   .GetAllNoTracking()
                                   .Any(c => c.CategoryId == validatableObject.CategoryId);

            if (!isCategoryCorrect)
            {
                validationErrors.Add(new ValidationError("Podana kategoria jest niepoprawna", nameof(Product.CategoryId)));
            }

            return new ValidationResult(validationErrors);
        }
    }
}