using MediatR;
using Prismo.Core.Features.Companies.GetCompanyById;

namespace Prismo.Api.Endpoints.Companies;

public static class GetCompanyByIdEndpoint
{
    public static void MapGetCompanyById(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/companies/{id:guid}", async (
            Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCompanyByIdQuery(id), cancellationToken);

            return result is null
                ? Results.NotFound(new { Message = $"Company with ID {id} was not found" })
                : Results.Ok(result);
        })
        .WithName("GetCompanyById");
    }
}
