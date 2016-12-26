/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import ash.core.Entity;
import ash.tick.ITickProvider;
import ash.tick.FrameTickProvider;
import nanogarch.systems.RenderSystem;
import nanogarch.systems.CollisionSystem;
import nanogarch.systems.SystemPriorities;
import nanogarch.graphics.*;
import nanogarch.components.*;

import minject.Injector;

using tink.CoreApi;
import openfl.display.DisplayObjectContainer;

class Nanogarch
{
	
	@inject public var engine:Engine;
	// @inject public var gameController:GameController;
	// @inject public var positionSystem:MapPositionSystem;
	@inject public var collisionSystem:CollisionSystem;
	@inject public var renderSystem:RenderSystem;
	@inject public var creator:EntityCreator;
	@inject public var tickProvider:FrameTickProvider;
	@inject("GameDisplayObject") public var container:DisplayObjectContainer;
   
	public function new(){}

	public function initialize():Void
	{
		// engine.addSystem(gameController, SystemPriorities.NONE);
		// engine.addSystem(positionSystem, SystemPriorities.NONE);	
		engine.addSystem(renderSystem, SystemPriorities.NONE);
		engine.addSystem(collisionSystem, SystemPriorities.NONE);	
		var cell = creator.createWordCell();
		engine.addEntity(cell);
		// var map = creator.createMap();
		// var unit = creator.createUnit();
		// unit.get(MapPosition).moveTo(new Hex(-2,2,0));
		
		// engine.addEntity(map);
		// engine.addEntity(unit);
		// tickProvider.add(engine.update);
        
	}
	public function start():Void
    {
        tickProvider.start();
    }
	// public static function createMap(size:Int,scale:Float,orientation:Bool):HexMap
	// {
	// 	var hexes = HexGrid.hexagonalShape(size);
	// 	var grid = new HexGrid(scale,orientation,hexes);
	// 	var map:HexMap = new HexMap(grid);
	// 	return map;
	// }
	public static function configure(container:DisplayObjectContainer,viewWidth:Int,viewHeight:Int)
	{
		var injector:Injector = new Injector();
		// injector.map(HexMap,"MainMap").toValue(createMap(5,75.0,true));

		var config:GameConfig = new GameConfig();
		config.viewWidth = viewWidth;
		config.viewHeight = viewHeight;
		
		var styleConfig:StyleConfig = new StyleConfig();

		injector.map(GameConfig).toValue(config);
		injector.map(StyleConfig).toValue(styleConfig);
		injector.map(Engine).asSingleton();
		injector.map(EntityCreator).asSingleton();
		injector.map(Entity).toClass(Entity);
		injector.map(WorldCellView).toClass(WorldCellView);

		injector.map(Frame).toClass(Frame);
		injector.map(HexFrame).toClass(HexFrame);

		injector.map(Collider).toClass(Collider);
		injector.map(Display).toClass(Display);
		injector.map(Terrain).toClass(Terrain);

		
		// injector.map(WorldCreator).asSingleton();
		
		// injector.map(GameController).asSingleton();
		// injector.map(MapPositionSystem).asSingleton();

		injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(container);
		injector.map(RenderSystem).asSingleton();
		injector.map(CollisionSystem).asSingleton();
		



		// var hexSelectedSignal:Signals.HexSignal;
		// var hexSelectedSignalTrigger:Signals.HexSignalTrigger;
		// hexSelectedSignal = hexSelectedSignalTrigger = Signal.trigger();

		// injector.map(Signals.HexSignalTrigger,"HexSelectedSignalTrigger").toValue(hexSelectedSignalTrigger);
		// injector.map(Signals.HexSignal,"HexSelectedSignal").toValue(hexSelectedSignal);
		

		injector.map(FrameTickProvider).toValue(new FrameTickProvider(container));
		injector.map(Injector).toValue(injector);
		injector.map(Nanogarch).asSingleton();

		// injector.map(NanogarchContract).asSingleton();
		
		return injector;
	}
}




