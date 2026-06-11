using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prismo.Core.Features.Telemetry.Domain;

namespace Prismo.Core.Infrastructure.Persistence.Configurations;

public class RouterTelemetryConfiguration : IEntityTypeConfiguration<RouterTelemetry>
{
    public void Configure(EntityTypeBuilder<RouterTelemetry> builder)
    {
        builder.ToTable("RouterTelemtries");

        builder.HasKey(t => new {t.TimeStamp, t.Id});

        builder.Property(t => t.RouterId)
            .HasMaxLength(50)
            .IsRequired();

        // Fixear la config
        builder.Property(t => t.ChannelOccupancy5G)
            .IsRequired();

        builder.Property(t => t.PacketLossRate)
            .IsRequired();

        builder.HasIndex(t => t.RouterId);
    }
}
