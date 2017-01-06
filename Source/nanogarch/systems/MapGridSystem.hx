package nanogarch.systems;
import ash.core.Engine;
import ash.core.Entity;
import ash.core.NodeList;
import ash.core.System;
import ash.tools.ListIteratingSystem;

import nanogarch.model.MapGrid;
import nanogarch.model.MapCell;
import nanogarch.nodes.MapGridNode;
import nanogarch.geom.HexDirection;
import openfl.events.MouseEvent;
using tink.CoreApi; 
class MapGridSystem extends ListIteratingSystem<MapGridNode>
{
	@inject("MainMap") public var map:MapGrid;
    @inject public var overMapCellSignal:Signals.OverMapCellSignal;

    private var mouseOverListeners:Map<MapGridNode, CallbackLink>;
    private var mouseOutListeners:Map<MapGridNode, CallbackLink>;
    private var mouseClickListeners:Map<MapGridNode, CallbackLink>;
	private var moveListeners:Map<MapGridNode, CallbackLink>;


    // public var overMapCellSignal(default,null):Signal<Entity>;
    // public var outMapCellSignal(default,null):Signal<Entity>;
    // public var clickMapCellSignal(default,null):Signal<Entity>;

    // var overMapCellSignalTrigger:SignalTrigger<Entity>;
    // var outMapCellSignalTrigger:SignalTrigger<Entity>;
    // var clickMapCellSignalTrigger:SignalTrigger<Entity>;


    public function new()
    {
        super(MapGridNode, null, nodeAdded, nodeRemoved);
        // overMapCellSignal = overMapCellSignalTrigger = Signal.trigger();
        // outMapCellSignal = outMapCellSignalTrigger = Signal.trigger();
        // clickMapCellSignal = clickMapCellSignalTrigger = Signal.trigger();
           
    }
	override public function addToEngine(engine:Engine):Void
    {
        trace("Adding Map Grid System!");
        moveListeners = new Map<MapGridNode, CallbackLink>();

        mouseOverListeners = new Map<MapGridNode, CallbackLink>();
        mouseOutListeners = new Map<MapGridNode, CallbackLink>();
        mouseClickListeners = new Map<MapGridNode, CallbackLink>();
        
        var entityIterator = map.entityIterator();
        for (entity in entityIterator)
           engine.addEntity(entity);
        
        super.addToEngine(engine);
        trace("DONE Adding Map Grid System!");
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
        trace("Adding node" + node);
    	var mapCell:MapCell = map.get(node.position);
    	mapCell.addEntity(node.entity);

        var listener = onMouseOverGridNode.bind(node);
        var link = node.collider.mouseOver.handle(listener);
        mouseOverListeners.set(node,link);

        listener = onMouseOutGridNode.bind(node);
        link = node.collider.mouseOut.handle(listener);
        mouseOutListeners.set(node,link);

        listener = onMouseClickGridNode.bind(node);
        link = node.collider.mouseClick.handle(listener);
        mouseClickListeners.set(node,link);

    	
    }
    private function onMouseOverGridNode(node:MapGridNode,event:MouseEvent)
    {
        trace("Over: "+node.position);
        overMapCellSignal.trigger(node.entity);
    }
    private function onMouseOutGridNode(node:MapGridNode,event:MouseEvent)
    {
        trace("Out: "+node.position);
        // outMapCellSignalTrigger.trigger(node.entity);
    }
    private function onMouseClickGridNode(node:MapGridNode,event:MouseEvent)
    {
        trace("Click "+ node.position);
        // outMapCellSignalTrigger.trigger(node.entity);
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