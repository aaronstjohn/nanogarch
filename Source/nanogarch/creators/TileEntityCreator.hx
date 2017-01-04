
package nanogarch.creators;
import ash.core.Entity;
import minject.Injector;class TileEntityCreator implements ICreator<Entity>
{
	@inject public var injector:Injector;
	public function new(){}
	public function create():Entity
	{
		// return injector.getInstance(En);
	}
	public function toString():String
	{
		return "TileEntityCreator";
	}
}