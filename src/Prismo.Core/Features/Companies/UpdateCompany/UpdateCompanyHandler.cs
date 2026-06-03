using MediatR;
using Microsoft.EntityFrameworkCore;
using Prismo.Core.Features.Companies.Domain;
using Prismo.Core.Infrastructure.Persistence;

namespace Prismo.Core.Features.Companies.UpdateCompany;

public class UpdateCompanyHandler(PrismoDbContext context) : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResponse>
{
    public async Task<UpdateCompanyResponse> Handle(
    
        UpdateCompanyCommand command,
        CancellationToken cancellationToken
    )
    {
        var company = await context.Companies
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken)
            ?? throw new KeyNotFoundException("");

        company.UpadateName(command.Name);
        company.UpadateNit(command.Nit);

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateCompanyResponse(
            company.Id,
            company.Name,
            company.Nit,
            company.CreatedAt
        );
    }
}
