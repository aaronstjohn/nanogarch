package nanogarch.systems;
import ash.core.Engine;
import ash.tools.ListIteratingSystem;
import nanogarch.components.Frame;
import nanogarch.nodes.MapPositionNode;
import hex.Grid;
import minject.Injector;
import nanogarch.map.HexMap;
import Type;

class MapPositionSystem extends ListIteratingSystem<MapPositionNode>
{
	@inject("MainMap") public var map:HexMap;
    
	public function new()
    {
        super(MapPositionNode, null, nodeAdded, nodeRemoved);
        
    }
    override public function addToEngine(engine:Engine):Void
    {
    	trace("Adding Position system to engine ");
        super.addToEngine(engine);
    }

    override public function removeFromEngine(engine:Engine):Void
    {
    	trace("Removing Position system from engine ");
        super.removeFromEngine(engine);
        
    }
    private function nodeAdded(node:MapPositionNode):Void
    {
    	trace("Adding Node  to position system ");
        // map.get(node.position.x, node.position.y).entities.push(node.entity);

        // var listener = onNodeMoveRequessted.bind(node);
        // moveListeners.set(node, listener);
        // node.position.moveRequested.add(listener);
    }
     private function nodeRemoved(node:MapPositionNode):Void
    {
    	trace("Removing Node  from position system ");
        // map.get(node.position.x, node.position.y).entities.remove(node.entity);

        // var listener = moveListeners.get(node);
        // moveListeners.remove(node);
        // node.position.moveRequested.remove(listener);
    }
}