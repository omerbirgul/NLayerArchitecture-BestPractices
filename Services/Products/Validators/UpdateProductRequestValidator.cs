using FluentValidation;
using Services.Products.Dtos.Requests;

namespace Services.Products.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Product Name Can Not Be Null")
            .NotEmpty().WithMessage("Product Name Can Not be Empty")
            .Length(3, 10).WithMessage("Product Name Must Be 3-10 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        // CategoryId Validation
        RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("CategoryId Required!")
            .NotEmpty().WithMessage("CategoryId Required!")
            .GreaterThan(0).WithMessage("CategoryId Must Be greater Than 0");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock must be 1-100");
    }
}