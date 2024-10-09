using Api.Models.Dtos;
using FluentValidation;

namespace Api.Validations;

public class CarbonEmissionValidator : AbstractValidator<CarbonEmissionDto>
{
    public CarbonEmissionValidator()
    {
        RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Campo CompanyId debe ser un numero mayor a 0");
        RuleFor(x => x.CompanyId).NotNull().WithMessage("Campo CompanyId obligatorio");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Campo Description obligatorio");
        RuleFor(x => x.Description).MinimumLength(5).WithMessage("Campo Description debe tener una longitud valida");
        
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Campo Quantity debe ser un numero mayor a 0");
        RuleFor(x => x.Quantity).NotNull().WithMessage("Campo Quantity obligatorio");
        
        RuleFor(x => x.Type).NotEmpty().WithMessage("Campo Type obligatorio");
        RuleFor(x => x.Type).NotNull().WithMessage("Campo Type obligatorio");
        RuleFor(x => x.Type).MinimumLength(3).WithMessage("Campo Type debe tener una longitud valida");
    }
}