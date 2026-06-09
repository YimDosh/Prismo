using MediatR;

namespace Prismo.Core.Features.Companies.UpdateCompany;

public record UpdateCompanyRequest(string Name, string Nit);
public record UpdateCompanyCommand(Guid Id, string Name, string Nit) : IRequest<UpdateCompanyResponse>;
public record UpdateCompanyResponse(Guid Id, string Name, string Nit, DateTime CreatedAt);
