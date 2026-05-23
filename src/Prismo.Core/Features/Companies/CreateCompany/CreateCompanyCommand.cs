using MediatR;

namespace Prismo.Core.Features.Companies.CreateCompany;

public record CreateCompanyRequest(string Name, string Nit);
public record CreateCompanyCommand(string Name, string Nit) : IRequest<CreateCompanyResponse>;
public record CreateCompanyResponse(Guid Id, string Name, string Nit, DateTime CreatedAt);
