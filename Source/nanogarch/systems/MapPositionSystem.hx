package nanogarch.systems;
import ash.core.Engine;
import ash.tools.ListIteratingSystem;
import nanogarch.components.Frame;
import nanogarch.nodes.MapPositionNode;
// import hex.Grid;
import minject.Injector;
import nanogarch.map.HexMap;
import nanogarch.map.HexDirection;
import Type;
typedef MoveRequestListener = HexDirection -> Void;
class MapPositionSystem extends ListIteratingSystem<MapPositionNode>
{
	@inject("MainMap") public var map:HexMap;
    private var moveListeners:Map<MapPositionNode, MoveRequestListener>;

	public function new()
    {
        super(MapPositionNode, null, nodeAdded, nodeRemoved);
        moveListeners = new Map<MapPositionNode,MoveRequestListener>();
        
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
        if( map.cellMap.exists(node.position))
        {
            map.cellMap.get(node.position).entities.push(node.entity);   
        }
        else
        {
            throw "Can't add entity:  "+node.entity+" to position: "+node.position+" no map cell exists";
        }
    	// trace("Adding Node  to position system ");
     //    // trace ("Hex map is "+map);
     //    trace ("Cell map is "+map.cellMap);
     //    trace("node position is "+node.position);
     //    trace("EXISTS "+map.cellMap.exists(node.position));
     // /var hexCell = map.cellMap.get(node.position); //.entities.push(node.entity);
     //    // node.mapPosition.moveRequest.
        // if(map.exists)
        // map.cellMap.get(node.position.x, node.position.y).entities.push(node.entity);

        var listener = onNodeMoveRequested.bind(node);
        moveListeners.set(node, listener);
        node.mapPosition.moveRequest.handle(listener);
    }
    // private function onNodeMoveRequested(node:MapPositionNode,hexDir:HexDir)
    private function nodeRemoved(node:MapPositionNode):Void
    {
    	trace("Removing Node  from position system ");
        // map.get(node.position.x, node.position.y).entities.remove(node.entity);

        // var listener = moveListeners.get(node);
        // moveListeners.remove(node);
        // node.position.moveRequested.remove(listener);
    }
    private function onNodeMoveRequested(node:MapPositionNode, direction:HexDirection):Void
    {
        trace("Move requested!!!");
        // var position:Position = node.position;
        // var target:Vector = position.getAdjacentTile(direction);
        // if (map.get(target.x, target.y).numObstacles == 0)
        // {
        //     map.get(position.x, position.y).entities.remove(node.entity);
        //     map.get(target.x, target.y).entities.push(node.entity);
        //     position.moveTo(target.x, target.y);
        // }
    }
}