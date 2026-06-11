using MediatR;

namespace Prismo.Core.Features.Telemetry.IngestTelemetry;

public record IngestTelemetryRequest(
    Guid RouterId,
    double ChannelOccupancy5G,
    double ChannelOccupancy24G,
    int ConnectedDevices5G,
    int ConnectedDevices24G,
    double PacketLoss,
    DateTime TimeStamp
);

public record IngestTelemetryCommand(
    Guid RouterId,
    double ChannelOccupancy5G,
    double ChannelOccupancy24G,
    int ConnectedDevices5G,
    int ConnectedDevices24G,
    double PacketLoss,
    DateTime TimeStamp
) : IRequest<bool>;
