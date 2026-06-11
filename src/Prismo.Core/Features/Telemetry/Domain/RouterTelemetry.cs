namespace Prismo.Core.Features.Telemetry.Domain;

public class RouterTelemetry
{
    public Guid Id { get; private set; }
    public Guid RouterId { get; private set; }
    public double ChannelOccupancy5G { get; private set; }
    public double ChannelOccupancy24G { get; private set; }
    public int ConnectedDevices5G { get; private set; }
    public int ConnectedDevices24G { get; private set; }
    public double PacketLossRate { get; private set; }
    public DateTime TimeStamp { get; private set; }

    private RouterTelemetry() {}

    public static RouterTelemetry Create
    (
        Guid routerId,
        double channelOccupancy5G,
        double channelOccupancy24G,
        int connectedDevices5G,
        int connectedDevices24G,
        double packetLossRate,
        DateTime timeStamp
    )
    {
        // ValidaciÓn ante datos corruptos
        if (packetLossRate < 0 || packetLossRate > 100)
            throw new ArgumentException("Packet loss rate must be between 0 and 100 percent.");
            
        if (channelOccupancy5G < 0 || channelOccupancy24G < 0)
            throw new ArgumentException("Channel occupancy cannot be negative.");

        return new RouterTelemetry
        {
          Id = Guid.CreateVersion7(),
          RouterId = routerId,
          ChannelOccupancy5G = channelOccupancy5G,
          ChannelOccupancy24G = channelOccupancy24G,
          ConnectedDevices5G = connectedDevices5G,
          ConnectedDevices24G = connectedDevices24G,
          PacketLossRate = packetLossRate,
          TimeStamp = timeStamp
        };
    }
}
