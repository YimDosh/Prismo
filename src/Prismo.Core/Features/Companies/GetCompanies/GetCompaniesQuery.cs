using MediatR;

namespace Prismo.Core.Features.Companies.GetCompanies;

public record GetCompaniesResponse(
    Guid Id,
    string Name,
    string Nit,
    bool IsActive,
    DateTime CreatedAt
);
public record GetCompaniesQuery() : IRequest<IEnumerable<GetCompaniesResponse>>;