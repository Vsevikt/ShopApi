using FluentValidation;
using ShopApplication.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<ProductCreateDTO>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Назва товару обов'язкова")
                .MaximumLength(200)
                .WithMessage("Назва товару не може бути довшою за 200 символів");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Ціна товару обов'язкова");

            RuleFor(product => product.StockQty)
                .NotEmpty()
                .WithMessage("Кількість товару обов'язкова");

            RuleForEach(product => product.ImageUrls)
                .MaximumLength(500)
                .WithMessage("URL зображення не може бути довшим за 500 символів");
        }
    }
}
