using MediatR;
using Prismo.Core.Features.Companies.GetCompanies;

namespace Prismo.Api.Endpoints.Companies;

public static class GetCompaniesEndpoint
{
    public static void MapGetCompanies(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/companies", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCompaniesQuery(), cancellationToken);
            return Results.Ok(result);
        }).WithName("GetCompanies");
    }
}
