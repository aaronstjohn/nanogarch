package nanogarch.systems;
import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;
import nanogarch.EntityCreator;

class GameController extends System
{
	@inject public var creator:EntityCreator;
	override public function addToEngine(engine:Engine):Void
    {
        trace("Adding Game controller ");
        creator.createUnit();
        creator.createMap();
    }
    override public function update(time:Float):Void
    {

    }
    override public function removeFromEngine(engine:Engine):Void
    {
        trace("Removing Game controller ");
    }

}