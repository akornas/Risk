using System;
using System.Collections.Generic;

public interface IMapController
{
	event Action<MapTileData> OnTileSelectedEvent;
	event Action<MapTileData> OnTileDeselectedEvent;
	event Action<MapTileData> OnTileClickedEvent;

	public List<MapTile> Tiles { get; }
}
