using FluentValidation;
using MiniECommerce.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Validations.FluentValidation.Validators
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Ürün adı boş geçilemez!")
                .Length(2, 100)
                    .WithMessage("Ürün adı en az 2, en fazla 100 karakter olabilir!");

            RuleFor(p => p.AmountOfStock)
                .NotNull()
                    .WithMessage("Ürün stok miktarı boş geçilemez!")
                .GreaterThan(-1)
                    .WithMessage("Ürün stok miktarı negatif olamaz!");

            RuleFor(p => p.Price)
                .NotNull()
                    .WithMessage("Ürün fiyatı boş geçilemez!")
                .Must(x => x > -1)
                    .WithMessage("Ürün fiyatı negatif olamaz!");
                
        }
    }
}
