using MediatR;
using Microsoft.EntityFrameworkCore;
using Prismo.Core.Infrastructure.Persistence;

namespace Prismo.Core.Features.Companies.UpdateCompany;

public class UpdateCompanyHandler(PrismoDbContext context) : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResponse>
{
    public async Task<UpdateCompanyResponse> Handle(

        UpdateCompanyCommand request,
        CancellationToken cancellationToken
    )
    {
        var company = await context.Companies
            .FindAsync([request.Id], cancellationToken)
            ?? throw new KeyNotFoundException();

        if (company.Nit != request.Nit)
        {
            var nitExist = await context.Companies
                .AnyAsync(x => x.Nit == request.Nit && x.Id != request.Id, cancellationToken);
            if (nitExist)
                throw new InvalidOperationException($"The nit {request.Nit} already is registered by another company");
        }

        company.UpdateName(request.Name);
        company.UpdateNit(request.Nit);

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateCompanyResponse(
            company.Id,
            company.Name,
            company.Nit,
            company.CreatedAt
        );
    }
}
