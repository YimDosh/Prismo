using MediatR;

namespace Prismo.Core.Features.Companies.GetCompanyById;

public record GetCompanyByIdQuery(Guid Id) : IRequest<GetCompanyByIdResponse?>;

public record GetCompanyByIdResponse(
    Guid Id,
    string Name,
    string Nit,
    bool IsActive,
    DateTime CreatedAt
);

