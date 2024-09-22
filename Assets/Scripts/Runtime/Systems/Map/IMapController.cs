using System;
using System.Collections.Generic;

public interface IMapController
{
	event Action<MapTile> OnTileSelectedEvent;
	event Action<MapTile> OnTileDeselectedEvent;
	event Action<MapTile> OnTileClickedEvent;

	public List<MapTile> Tiles { get; }
}
