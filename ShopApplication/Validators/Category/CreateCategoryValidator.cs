using FluentValidation;
using ShopApplication.DTOs.Category;
using System;

public class CreateCategoryValidator : AbstractValidator<CategoryCreateDTO>
{
    public CreateCategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage("Назва категорії обов'язкова")
            .MaximumLength(20)
            .WithMessage("Назва категорії не може бути довшою за 10 символів");

        RuleFor(category => category.Slug)
            .NotEmpty()
            .WithMessage("Slug категорії обов'язковий")
            .MaximumLength(20)
            .WithMessage("Slug не може бути довшим за 20 символів");

        RuleFor(category => category.Url)
            .MaximumLength(500)
            .WithMessage("URL не може бути довшим за 500 символів");
    }
}