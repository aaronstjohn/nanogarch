package nanogarch.commands;
import ash.core.Engine;
import nanogarch.systems.MapGridSystem;
import nanogarch.systems.RenderSystem;
import nanogarch.systems.SystemPriorities;

import ash.tick.FrameTickProvider;
using tink.CoreApi; 
class StartCommand extends Command<Noise>
{
	@inject public var engine:Engine;
	
	@inject public var mapGridSystem:MapGridSystem;
	@inject public var renderSystem:RenderSystem;
	@inject public var tickProvider:FrameTickProvider;

	public function new(){super();}

	override public function Execute(arg:Noise)
	{
		trace("START COMMAND EXECUTED!!");
		engine.addSystem(mapGridSystem, SystemPriorities.LOGIC);
    	engine.addSystem(renderSystem, SystemPriorities.RENDER);
    	tickProvider.add(engine.update);
        tickProvider.start();
	}
}