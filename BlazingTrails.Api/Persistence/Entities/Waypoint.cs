
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Entities;

// In chapter 8, pg. 232, Waypoint is replaces RouteInstructions
// This is a bit confusing because in  in Appendix A, page 319, RouteInstructions is used
public class Waypoint
{
	public int Id { get; set; }
	public int TrailId { get; set; }
	public decimal Latitude { get; set; }
	public decimal Longitude { get; set; }

	public Trail Trail { get; set; } = default!;
}

public class WaypointConfig : IEntityTypeConfiguration<Waypoint>
{
	public void Configure(EntityTypeBuilder<Waypoint> builder)
	{
		builder.Property(x => x.TrailId).IsRequired();
		builder.Property(x => x.Latitude).IsRequired();
		builder.Property(x => x.Longitude).IsRequired();
	}
}

// Ignore Spelling: Waypoint