using System;
using System.Collections.Generic;

public interface IMapController
{
	event Action OnSetActiveEvent;
	event Action<MapTile> OnTileSelectedEvent;
	event Action<MapTile> OnTileDeselectedEvent;
	event Action<MapTile> OnTileClickedEvent;

	public List<MapTile> Tiles { get; }
	public bool IsActive { get; }
}
