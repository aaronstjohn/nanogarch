/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import ash.tick.ITickProvider;
import ash.tick.FrameTickProvider;
import nanogarch.systems.GameController;
import nanogarch.systems.MapPositionSystem;
import nanogarch.components.MapPosition;
import nanogarch.systems.RenderSystem;
import nanogarch.systems.SystemPriorities;
import nanogarch.map.HexGrid;
import nanogarch.map.HexMap;
import nanogarch.map.Hex;
import minject.Injector;

using tink.CoreApi;
// import hex.*;
import openfl.display.DisplayObjectContainer;

class Nanogarch
{
	
	@inject public var engine:Engine;
	@inject public var gameController:GameController;
	@inject public var positionSystem:MapPositionSystem;
	@inject public var renderSystem:RenderSystem;
	@inject public var creator:EntityCreator;
	@inject public var tickProvider:FrameTickProvider;
	@inject("GameDisplayObject") public var container:DisplayObjectContainer;
   
	public function new(){}

	public function initialize():Void
	{
		engine.addSystem(gameController, SystemPriorities.NONE);
		engine.addSystem(positionSystem, SystemPriorities.NONE);	
		engine.addSystem(renderSystem, SystemPriorities.NONE);	

		var map = creator.createMap();
		var unit = creator.createUnit();
		unit.get(MapPosition).moveTo(new Hex(-2,2,0));
		
		engine.addEntity(map);
		engine.addEntity(unit);
		tickProvider.add(engine.update);
        
	}
	public function start():Void
    {
        tickProvider.start();
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
		injector.map(HexMap,"MainMap").toValue(createMap(5,75.0,true));

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


		var hexSelectedSignal:Signals.HexSignal;
		var hexSelectedSignalTrigger:Signals.HexSignalTrigger;
		hexSelectedSignal = hexSelectedSignalTrigger = Signal.trigger();

		injector.map(Signals.HexSignalTrigger,"HexSelectedSignalTrigger").toValue(hexSelectedSignalTrigger);
		injector.map(Signals.HexSignal,"HexSelectedSignal").toValue(hexSelectedSignal);
		

		injector.map(FrameTickProvider).toValue(new FrameTickProvider(container));
		injector.map(Injector).toValue(injector);
		injector.map(Nanogarch).asSingleton();

		// injector.map(NanogarchContract).asSingleton();
		
		return injector;
	}
}




