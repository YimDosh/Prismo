namespace Prismo.Core.Features.Telemetry.Domain;

public class RouterTelemetry
{
    public Guid Id { get; private set; }
    public string RouterId { get; private set; } = null!;
    public double ChannelOccupancy { get; private set; }
    public int ConnectedDevices { get; private set; }
    public double PacketLossRate { get; private set; }
    public DateTime TimeStamp { get; private set; }

    private RouterTelemetry() {}

    public static RouterTelemetry Create
    (
        string routerId,
        double channeloccupancy,
        int connectedDevices,
        double packetLossRate
    )
    {
        return new RouterTelemetry
        {
          Id = Guid.CreateVersion7(),
          RouterId = routerId,
          ChannelOccupancy = channeloccupancy,
          ConnectedDevices = connectedDevices,
          PacketLossRate = packetLossRate,
          TimeStamp = DateTime.UtcNow
        };
    }
}
