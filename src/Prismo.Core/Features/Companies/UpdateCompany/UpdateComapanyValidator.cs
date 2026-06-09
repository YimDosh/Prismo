using FluentValidation;

namespace Prismo.Core.Features.Companies.UpdateCompany;

public class UpdateComapanyValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateComapanyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("This field can't be empty")
            .MinimumLength(3).WithMessage("Minimun 3 charcters");

        RuleFor(x => x.Nit)
            .NotEmpty().WithMessage("The nit is required")
            .Matches(@"^[0-690-9]+$").WithMessage("This nit is invalid")
            .MinimumLength(6).WithMessage("The nit requires 6 characters min")
            .MaximumLength(8).WithMessage("The nit requires 8 characters max");
    }
   
}
