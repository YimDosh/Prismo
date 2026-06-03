using MediatR;
using Prismo.Core.Features.Companies.CreateCompany;

namespace Prismo.Api.Endpoints.Companies;

public static class CreateCompanyEndpoint
{
    public static void MapCreateCompany(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/companies", async (CreateCompanyCommand command, ISender mediator, CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(command, cancellationToken);
        }).WithName("CreateCompany");
    }
}
