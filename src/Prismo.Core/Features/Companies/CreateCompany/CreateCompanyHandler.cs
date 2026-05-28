using MediatR;
using Prismo.Core.Features.Companies.Domain;
using Prismo.Core.Infrastructure.Persistence;

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