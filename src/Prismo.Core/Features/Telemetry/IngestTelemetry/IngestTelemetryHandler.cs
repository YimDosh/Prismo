using MediatR;
using Prismo.Core.Features.Telemetry.Domain;
using Prismo.Core.Infrastructure.Persistence;

namespace Prismo.Core.Features.Telemetry.IngestTelemetry;

public class IngestTelemetryHandler(PrismoDbContext context) : IRequestHandler<IngestTelemetryCommand, bool>
{
    public async Task<bool> Handle(IngestTelemetryCommand request, CancellationToken cancellationToken)
    {
        var telemetry = RouterTelemetry.Create(
            request.RouterId,
            request.ChannelOccupancy24G,
            request.ChannelOccupancy5G,
            request.ConnectedDevices24G,
            request.ConnectedDevices5G,
            request.PacketLoss,
            request.TimeStamp
        );
        await context.RouetrTelemetry.AddAsync(telemetry, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }    
}
