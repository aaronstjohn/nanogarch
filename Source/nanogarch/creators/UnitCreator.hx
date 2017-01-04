package nanogarch.creators;
import nanogarch.model.MapCell;
import minject.Injector;

class UnitCreator implements ICreator<Entity>
{
	@inject public var injector:Injector;
	public function new(){}
	public function create():Entity
	{
		// return injector.getInstance(En);
	}
	public function toString():String
	{
		return "UnitCreator";
	}
}