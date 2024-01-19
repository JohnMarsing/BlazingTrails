using BlazingTrails.Client.Features.Shared;
using BlazingTrails.Shared.Features.Home.Shared;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazingTrails.Client.Features.Home;

public partial class HomePage
{
	[Inject] public ILogger<HomePage>? Logger { get; set; }

	private IEnumerable<Trail>? _trails;
	private Trail? _selectedTrail;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogInformation("Inside {Class}!{Method}", nameof(HomePage), nameof(OnInitializedAsync));
		try
		{
			var response = await Mediator.Send(new GetTrailsRequest());
			//_trails = new List<Trail>();
			_trails = response.Trails.Select(x => new Trail
			{
				Id = x.Id,
				Name = x.Name,
				Image = x.Image,
				Description = x.Description,
				Location = x.Location,
				Length = x.Length,
				TimeInMinutes = x.TimeInMinutes,
				Owner = x.Owner,
				Waypoints = x.Waypoints.Select(wp => new BlazingTrails.ComponentLibrary.Map.LatLong(wp.Latitude, wp.Longitude))?.ToList() ?? new List<ComponentLibrary.Map.LatLong>()
			});
			Logger!.LogDebug("...database call successfully made");
		}
		catch (HttpRequestException ex)
		{
			Logger?.LogError(ex, "...Exception type {ExceptionType} occurred inside {Class}!{Method}; There was a problem loading trail data"
			, nameof(HttpRequestException), nameof(HomePage), nameof(OnInitializedAsync));
		}
		catch (Exception ex)
		{
			Logger?.LogError(ex, "...Exception occurred inside {Class}!{Method}"
				, nameof(HomePage), nameof(OnInitializedAsync));
		}
	}

	private void HandleTrailSelected(Trail trail)
			=> _selectedTrail = trail;
}