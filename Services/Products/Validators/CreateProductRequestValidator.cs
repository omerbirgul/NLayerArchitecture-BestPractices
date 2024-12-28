using FluentValidation;
using Services.Products.Dtos.Requests;

namespace Services.Products.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Product Name Required!")
            .NotEmpty().WithMessage("Product Name Required!")
            .Length(3, 10).WithMessage("Product Name Length Must Be 3-10 Characters");
        
        // Price Validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product Price Must Be Greater Than 0");
        
        // Stock Validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock count must be between 1-100");
    }
}