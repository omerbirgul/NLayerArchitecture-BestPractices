using FluentValidation;
using Repositories.GenericRepository.ProductRepositories;
using Services.Products.Dtos.Requests;

namespace Services.Products.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Product Name Required!")
            .NotEmpty().WithMessage("Product Name Required!")
            .Length(3, 10).WithMessage("Product Name Length Must Be 3-10 Characters");
            // .Must(MustUniqueProductName).WithMessage("Product Name Already Exist");
        
        // Price Validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product Price Must Be Greater Than 0");
        
        // CategoryId Validation
        RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("CategoryId Required!")
            .NotEmpty().WithMessage("CategoryId Required!")
            .GreaterThan(0).WithMessage("CategoryId Must Be greater Than 0");
        
        // Stock Validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock count must be between 1-100");
    }


    #region Sync Way Business Validation

    // private bool MustUniqueProductName(string productName)
    // {
    //     return !(_productRepository.Where(p => p.Name == productName).Any());
    //     // false => hata var
    //     // true => hata yok
    // }

    #endregion
}