package nanogarch.creators;
import nanogarch.model.MapCell;
import minject.Injector;

class MapCellCreator implements ICreator<MapCell>
{
	@inject public var injector:Injector;
	public function new(){}
	public function create():MapCell
	{
		return injector.getInstance(MapCell);
	}
	public function toString():String
	{
		return "MapCellCreator";
	}
}