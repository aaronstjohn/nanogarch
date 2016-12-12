/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import nanogarch.systems.GameController;
import nanogarch.systems.PositionSystem;
import nanogarch.systems.SystemPriorities;
import minject.Injector;
import hex.*;
class Nanogarch
{
	
	@inject public var engine:Engine;
	@inject public var gameController:GameController;
	@inject public var positionSystem:PositionSystem;
   
	public function new(){}

	public function initialize():Void
	{
		engine.addSystem(gameController, SystemPriorities.NONE);
		engine.addSystem(positionSystem, SystemPriorities.NONE);	
	}
	public static function configure()
	{
		var injector:Injector = new Injector();
		var hexes:Array<Cube> = Grid.hexagonalShape(5);
		var grid:Grid  = new Grid(1.0,true,hexes);
		injector.map(Grid,"MainGrid").toValue(grid);
		
		injector.map(Engine).asSingleton();
		injector.map(EntityCreator).asSingleton();
		
		injector.map(GameController).asSingleton();
		injector.map(PositionSystem).asSingleton();

		injector.map(Injector).toValue(injector);
		injector.map(Nanogarch).asSingleton();
		return injector;
	}
}




