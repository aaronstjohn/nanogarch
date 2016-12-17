/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import nanogarch.systems.GameController;
import nanogarch.systems.MapPositionSystem;
import nanogarch.components.MapPosition;
import nanogarch.systems.RenderSystem;
import nanogarch.systems.SystemPriorities;
import nanogarch.map.HexGrid;
import nanogarch.map.HexMap;
import minject.Injector;
import hex.*;
import openfl.display.DisplayObjectContainer;

class Nanogarch
{
	
	@inject public var engine:Engine;
	@inject public var gameController:GameController;
	@inject public var positionSystem:MapPositionSystem;
	@inject public var renderSystem:RenderSystem;
	@inject public var creator:EntityCreator;
   
	public function new(){}

	public function initialize():Void
	{
		engine.addSystem(gameController, SystemPriorities.NONE);
		engine.addSystem(positionSystem, SystemPriorities.NONE);	
		engine.addSystem(renderSystem, SystemPriorities.NONE);	

		var map = creator.createMap();
		var unit = creator.createUnit();
		unit.get(MapPosition).moveTo(new Hex(-2,2,0));
		engine.addEntity(unit);
		engine.addEntity(map);
        
	}
	public static function createMap(size:Int,scale:Float,orientation:Bool):HexMap
	{
			var hexes = HexGrid.hexagonalShape(size);
			var grid = new HexGrid(scale,orientation,hexes);
			var map:HexMap = new HexMap(grid);
			return map;
			
	}
	public static function configure(container:DisplayObjectContainer,viewWidth:Int,viewHeight:Int)
	{
		var injector:Injector = new Injector();
		injector.map(HexMap,"MainMap").toValue(createMap(5,100.0,false));

		var config:GameConfig = new GameConfig();
		config.viewWidth = viewWidth;
		config.viewHeight = viewHeight;
	

		injector.map(GameConfig).toValue(config);
		injector.map(Engine).asSingleton();
		injector.map(EntityCreator).asSingleton();
		
		injector.map(GameController).asSingleton();
		injector.map(MapPositionSystem).asSingleton();

		injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(container);
		injector.map(RenderSystem).asSingleton();


		
		injector.map(Injector).toValue(injector);
		injector.map(Nanogarch).asSingleton();

		// injector.map(NanogarchContract).asSingleton();
		
		return injector;
	}
}




