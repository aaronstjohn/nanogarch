package nanogarch.systems;
import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;
import ash.tools.ListIteratingSystem;
import nanogarch.model.MapGrid;
import nanogarch.model.MapCell;
import nanogarch.nodes.MapGridPositionNode;
import nanogarch.geom.HexDirection;
import nanogarch.nodes.MovementNode;
using tink.CoreApi; 
class MovementQuerySystem extends ListIteratingSystem<MovementNode>
{
	@inject public var map:MapGrid;
	@inject public var movementQuerySignal:Signals.MovementQuery;

	// public var movementSignals:NodeSignalManager<MovementNode>;
	// private var queryListeners:Map<MovementNode, CallbackLink>;

	public function new()
    {
        super(MovementNode, null, nodeAdded, nodeRemoved);
        // querySignals = new CallbackMgr<MovementNode>();
        // querySignals.handle(onMovementQuery,function(n:MovementNode)
        // 	{
        // 		return n.movement.queryMovement;
        // 	});
    }
	override public function addToEngine(engine:Engine):Void
	{
		
		trace("Added MovementQuerySystem System");

	}
	public function queryMovementRange(node:MovementNode):Array<Hex>
	{

	}
	override public function removeFromEngine(engine:Engine):Void
	{
		
		trace("Removed MovementQuerySystem System");
	}
	public function nodeAdded(node:MovementNode)
	{
		// queryCallbacks.addNode(node);
	}
	public function nodeRemoved(node:MovementNode)
	{
		// queryCallbacks.removeNode(node);
	}
}