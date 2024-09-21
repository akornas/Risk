using System;

public interface IMapController
{
	event Action<MapTileData> OnTileSelectedEvent;
	event Action<MapTileData> OnTileDeselectedEvent;
	event Action<MapTileData> OnTileClickedEvent;
}
