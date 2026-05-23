using FluentValidation;

namespace Prismo.Core.Features.Companies.CreateCompany;

public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The company name is required.")
            .MinimumLength(3).WithMessage("Minimun 3 characters");

        RuleFor(x => x.Nit)
            .NotEmpty().WithMessage("The nit is required")
            .Matches(@"^[0-690-9]+$").WithMessage("This nit is invalid");
    }
}
