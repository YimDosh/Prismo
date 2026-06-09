using MediatR;
using Prismo.Core.Features.Companies.UpdateCompany;

namespace Prismo.Api.Endpoints.Companies;

public static class UpdateCompanyEndpoint
{
    public static void MapPatchUpdateCompany(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/companies/{id:guid}", async (
            Guid id,
            UpdateCompanyRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateCompanyCommand(id, request.Name, request.Nit);
            var result = await sender.Send(command, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("UpdateCompany");
    }
}
