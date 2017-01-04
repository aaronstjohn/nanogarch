/* A simple multiplayer strategy game */
package nanogarch;
import ash.core.Engine;
import ash.core.Entity;
import ash.tick.ITickProvider;
import ash.tick.FrameTickProvider;
import nanogarch.creators.MapCellCreator;
import nanogarch.util.InjectiveEntity;
import nanogarch.systems.*;
import nanogarch.graphics.*;
import nanogarch.components.*;
import nanogarch.model.*;
// import Signals;

import minject.Injector;

using tink.CoreApi;
import openfl.display.DisplayObjectContainer;

class Nanogarch
{
	
	@inject public var engine:Engine;
	@inject public var injector:Injector;

	// @inject public var gameController:GameController;
	@inject public var mapGridSystem:MapGridSystem;
	// @inject public var collisionSystem:CollisionSystem;
	@inject public var renderSystem:RenderSystem;
	// @inject public var creator:EntityCreator;
	// @inject public var mapGrid:MapGrid; 
	@inject public var tickProvider:FrameTickProvider;
	// @inject("GameDisplayObject") public var container:DisplayObjectContainer;
   
	public function new(){}


	public function start():Void
    {
    	// trace("Engine "+engine);
    	// trace("Mapsystem "+mapGridSystem);
    	engine.addSystem(mapGridSystem, SystemPriorities.LOGIC);
    	engine.addSystem(renderSystem, SystemPriorities.RENDER);
    	tickProvider.add(engine.update);
        tickProvider.start();
    }

	
}




