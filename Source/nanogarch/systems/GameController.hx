package nanogarch.systems;
import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;
import nanogarch.EntityCreator;
import minject.Injector;
import nanogarch.map.Hex;
class GameController extends System
{
    @inject("HexSelectedSignal") public var hexSelected:Signals.HexSignal;
   
	@inject public var creator:EntityCreator;
    // @inject public var contract:NanogarchContract;
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
        // var currentState:GameState = injector.getInstance(GameState,'CurrentState');
        

    }
	override public function addToEngine(engine:Engine):Void
    {
        trace("Adding Game controller ");
        hexSelected.handle(onHexSelected);
        // contract.turnResolved.handle(handleTurnResolved);
        
    }
    function onHexSelected(hex:Hex)
    {
        trace ("HEX SELECTED "+hex);
    }
    override public function update(time:Float):Void
    {

    }
    override public function removeFromEngine(engine:Engine):Void
    {
        trace("Removing Game controller ");
    }

}