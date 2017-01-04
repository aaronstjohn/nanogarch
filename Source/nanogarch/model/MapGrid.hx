package nanogarch.model;
import nanogarch.creators.MapCellCreator;
// import minject.Injector;
import nanogarch.geom.Hex;
import nanogarch.components.MapGridFrame;
import ash.core.Entity;

class MapGrid extends HexGrid<MapCell>
{
	// @inject public var injector:Injector;
	@inject public function new(creator:MapCellCreator)
	{
		super(creator);
	}
	public function addEntity(hex:Hex,entity:Entity)
	{
		// if (!entity.has(HexFrame))
		// 	entity.add(injector.getInstance(HexFrame));
		
		//Force Error if HexFrame doesn't exist!
		entity.get(MapGridFrame).set(hex);
		
		var mapCell:MapCell = contentMap.get(hex);
		mapCell.addEntity(entity);
	}
	public function entityIterator():Iterator<Entity>
	{
		return new MapGridIterator(this);
		
	}
	// public function initialize()
	// {
	// 	this.contentProvider=cellCreator;
	// 	return this;
	// }
}
class MapGridIterator
{
	var mapGrid:MapGrid; 
	var hexes:Iterator<Hex>;
	var entities:Iterator<Entity>;
	public function new(mapGrid:MapGrid)
	{
		this.mapGrid = mapGrid;
		hexes = mapGrid.hexIterator();
		entities = null;

	}
	public function hasNext():Bool {
		if( (entities == null || !entities.hasNext()) && hexes.hasNext())
		{
			var hex = hexes.next();
			var cell = mapGrid.get(hex);
			entities = cell.iterator();
			return entities.hasNext();
		}
		return false;
	}

	public function next():Entity {
		return entities.next();
	}
}