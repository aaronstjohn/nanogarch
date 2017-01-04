package nanogarch.systems;
import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;
import ash.tools.ListIteratingSystem;
import nanogarch.model.MapGrid;
import nanogarch.model.MapCell;
import nanogarch.nodes.MapGridNode;
import nanogarch.geom.HexDirection;
using tink.CoreApi; 
class MapGridSystem extends ListIteratingSystem<MapGridNode>
{
	@inject("MainMap") public var map:MapGrid;
	private var moveListeners:Map<MapGridNode, CallbackLink>;

    public function new()
    {
        super(MapGridNode, null, nodeAdded, nodeRemoved);
    }
	override public function addToEngine(engine:Engine):Void
    {
        moveListeners = new Map<MapGridNode, CallbackLink>();
        var entityIterator = map.entityIterator();
        for (entity in entityIterator)
           engine.addEntity(entity);
        
        super.addToEngine(engine);
    }

    override public function removeFromEngine(engine:Engine):Void
    {
        super.removeFromEngine(engine);

        // for (node in moveListeners.keys())
        //     node.position.moveRequested.remove(moveListeners.get(node));
        // moveListeners = null;
    }

	private function nodeAdded(node:MapGridNode):Void
    {
    	var mapCell:MapCell = map.get(node.position);
    	mapCell.addEntity(node.entity);
    	// trace("Node added to hexCell: "+node.position);
       
        // var listener = onNodeMoveRequested.bind(node);
        // var link = node.hexFrame.moveRequested.handle(listener);
        // moveListeners.set(node, link);
        
    }
    private function onNodeMoveRequested(node:MapGridNode, direction:HexDirection):Void
    {
    	trace ("IMPLEMENT MOVING!!!");
        // var position:Position = node.position;
        // var target:Vector = position.getAdjacentTile(direction);
        // if (map.get(target.x, target.y).numObstacles == 0)
        // {
        //     map.get(position.x, position.y).entities.remove(node.entity);
        //     map.get(target.x, target.y).entities.push(node.entity);
        //     position.moveTo(target.x, target.y);
        // }
    }
    private function nodeRemoved(node:MapGridNode):Void
    {
    	var mapCell:MapCell = map.get(node.position);
    	mapCell.removeEntity(node.entity);
        // map.get(node.position.x, node.position.y).entities.remove(node.entity);

        // var link = moveListeners.get(node);
        // moveListeners.remove(node);
        // link.dissolve();
        // node.position.moveRequested.remove(listener);
    }
    // private function onNodeMoveRequested(node:PositionNode, direction:Direction):Void
    // {
    //     var position:Position = node.position;
    //     var target:Vector = position.getAdjacentTile(direction);
    //     if (map.get(target.x, target.y).numObstacles == 0)
    //     {
    //         map.get(position.x, position.y).entities.remove(node.entity);
    //         map.get(target.x, target.y).entities.push(node.entity);
    //         position.moveTo(target.x, target.y);
    //     }
    // }

    
	

}