using Microsoft.EntityFrameworkCore;
using MediatR;
using Prismo.Core.Features.Companies.Domain;
using Prismo.Core.Infrastructure.Persistence;
using FluentValidation;

namespace Prismo.Core.Features.Companies.CreateCompany;

// Usando Primary Constructor de C# moderno
public class CreateCompanyHandler(PrismoDbContext context) 
    : IRequestHandler<CreateCompanyCommand, CreateCompanyResponse>
{
    public async Task<CreateCompanyResponse> Handle(
        CreateCompanyCommand request, 
        CancellationToken cancellationToken
    )
    {
        var nitExist = await context.Companies
            .AnyAsync(n => n.Nit == request.Nit, cancellationToken);

        if (nitExist)
        {
            //  Creamos un fallo estructurado asignado a la propiedad "Nit"
            var failure = new FluentValidation.Results.ValidationFailure(
                nameof(request.Nit), 
                $"The NIT '{request.Nit}' is already registered."
            );

            throw new ValidationException([failure]);
        }
        // 1. Instanciamos el Dominio usando tu Factory Method semántico
        var company = Company.Create(request.Name, request.Nit);

        // 2. Lo guardamos en PostgreSQL vía EF Core
        context.Companies.Add(company);
        await context.SaveChangesAsync(cancellationToken);

        // 3. Retornamos el Response DTO mapeado limpiamente
        return new CreateCompanyResponse(
            company.Id, 
            company.Name, 
            company.Nit, 
            company.CreatedAt
        );
    }
}