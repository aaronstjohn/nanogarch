package nanogarch.creators;
import ash.core.Entity;
import nanogarch.model.MapCell;
import nanogarch.model.MapGrid;
import nanogarch.geom.Hex;
import nanogarch.model.HexGrid;
import minject.Injector;

class MapGridCreator  implements ICreator<MapGrid>
{
	@inject public var injector:Injector;
	public function new()
	{

	}

	public function create():MapGrid
	{
		//Grab the base map instance 
		var map:MapGrid = injector.getInstance(MapGrid);

		//First generate a grid shape layout 
		var hexShapeGrid:Array<Hex> = HexGrid.hexagonalShape(15);

		//This generates a grid of empty hexes 
		map.addHexes(hexShapeGrid);

		var tileCreator:RandomTileCreator = injector.getInstance(RandomTileCreator);
		
		for (hex in hexShapeGrid)
		{
			var tile:Entity = tileCreator.create();
			map.addEntity(hex,tile);
		}
		


		return map;
	}
	public function toString():String
	{
		return "MapCellCreator";
	}
}