package nanogarch.systems;
import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;
import nanogarch.EntityCreator;
import minject.Injector;

class GameController extends System
{
	@inject public var creator:EntityCreator;
    @inject public var contract:NanogarchContract;
    @inject public var injector:Injector;
    // private var orderQueue:Array<Order>;

    public function new()
    {
        super();
        // orderQueue=[];
    }
    public function handleTurnResolved():Void
    {
        trace("TURN RESOLVED!!");
        var currentState:GameState = injector.getInstance(GameState,'CurrentState');
        

    }
	override public function addToEngine(engine:Engine):Void
    {
        trace("Adding Game controller ");
        contract.turnResolved.handle(handleTurnResolved);
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