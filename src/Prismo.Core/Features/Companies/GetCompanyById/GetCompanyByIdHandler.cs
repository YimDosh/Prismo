using MediatR;
using Microsoft.EntityFrameworkCore;
using Prismo.Core.Infrastructure.Persistence;

namespace Prismo.Core.Features.Companies.GetCompanyById;

public class GetCompanyByIdHandler(PrismoDbContext context) : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdResponse?>
{
    public async Task<GetCompanyByIdResponse?> Handle(
        GetCompanyByIdQuery request, CancellationToken cancellationToken
    )
    {
        var company = await context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if(company is null)
            return null;

        return new GetCompanyByIdResponse(
            company.Id,
            company.Name,
            company.Nit,
            company.IsActive,
            company.CreatedAt
        );
    }
}