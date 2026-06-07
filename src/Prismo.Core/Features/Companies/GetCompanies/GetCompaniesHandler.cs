using MediatR;
using Microsoft.EntityFrameworkCore;
using Prismo.Core.Infrastructure.Persistence;

namespace Prismo.Core.Features.Companies.GetCompanies;

public class GetCompaniesHandler(PrismoDbContext context) 
    : IRequestHandler<GetCompaniesQuery, IEnumerable<GetCompaniesResponse>>
{
    public async Task<IEnumerable<GetCompaniesResponse>> Handle(
        GetCompaniesQuery request, 
        CancellationToken cancellationToken
    )
    {
        return await context.Companies
            .AsNoTracking()
            .Select(x => new GetCompaniesResponse(
                x.Id,
                x.Name,
                x.Nit,
                x.IsActive,
                x.CreatedAt
            )).ToListAsync(cancellationToken);
    }
}
