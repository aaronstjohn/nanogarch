/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import nanogarch.systems.GameController;
import nanogarch.systems.PositionSystem;
import nanogarch.systems.RenderSystem;
import nanogarch.systems.SystemPriorities;
import minject.Injector;
import hex.*;
import openfl.display.DisplayObjectContainer;

class Nanogarch
{
	
	@inject public var engine:Engine;
	@inject public var gameController:GameController;
	@inject public var positionSystem:PositionSystem;
	@inject public var renderSystem:RenderSystem;
   
	public function new(){}

	public function initialize():Void
	{
		engine.addSystem(gameController, SystemPriorities.NONE);
		engine.addSystem(positionSystem, SystemPriorities.NONE);	
		engine.addSystem(renderSystem, SystemPriorities.NONE);	
	}
	public static function configure(container:DisplayObjectContainer)
	{
		var injector:Injector = new Injector();
		var hexes:Array<Cube> = Grid.hexagonalShape(5);
		var grid:Grid  = new Grid(1.0,true,hexes);
		injector.map(Grid,"MainGrid").toValue(grid);
		
		injector.map(Engine).asSingleton();
		injector.map(EntityCreator).asSingleton();
		
		injector.map(GameController).asSingleton();
		injector.map(PositionSystem).asSingleton();

		injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(container);
		injector.map(RenderSystem).asSingleton();

		injector.map(Injector).toValue(injector);
		injector.map(Nanogarch).asSingleton();
		return injector;
	}
}




