package nanogarch.systems;

import openfl.display.DisplayObject;
import openfl.display.DisplayObjectContainer;
import openfl.geom.Matrix;

import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;

import nanogarch.components.Display;
import nanogarch.components.Frame;
import nanogarch.components.HexFrame;
import nanogarch.nodes.CollisionNode;
// import nanogarch.nodes.TransformNode;

import minject.Injector;
import nanogarch.GameConfig;
import hxmath.frames.Frame2;
import openfl.events.MouseEvent;
using tink.CoreApi; 
class CollisionSystem extends System
{
	@inject("GameDisplayObject") public var container:DisplayObjectContainer;
	private var mouseOverListeners:Map<CollisionNode, CallbackLink>;
	private var mouseOutListeners:Map<CollisionNode, CallbackLink>;
	private var collisionNodes:NodeList<CollisionNode>;
	public function new(){
		super();
		mouseOverListeners = new Map<CollisionNode, CallbackLink>();
		mouseOutListeners = new Map<CollisionNode, CallbackLink>();
	}

	override public function addToEngine(engine:Engine):Void
	{
		
		collisionNodes = engine.getNodeList(CollisionNode);
		for (node in collisionNodes)
		    addCollisionNode(node);
		collisionNodes.nodeAdded.add(addCollisionNode);
		collisionNodes.nodeRemoved.add(removeCollisionNode);

		trace("Added Collision System");
	}
	private function addCollisionNode(node:CollisionNode)
    {
    	container.addChild(node.collider.collisionShape);
    	// node.collider.registerSignals();
    	node.collider.collisionShape.x = node.x;
    	node.collider.collisionShape.y= node.y;
    	
        var listener = onMouseOverCollision.bind(node);
        var link = node.collider.mouseOver.handle(listener);
        mouseOverListeners.set(node,link);

        listener = onMouseOutCollision.bind(node);
        link = node.collider.mouseOut.handle(listener);
        mouseOutListeners.set(node,link);
    
    }
    private function onMouseOverCollision(node:CollisionNode,event:MouseEvent)
    {
        trace("Mouse Over!"+event);
        // node.relay.relay(event.type,node.entity);
    }
    private function onMouseOutCollision(node:CollisionNode,event:MouseEvent)
    {
        trace("Mouse Out!");
    }
  
    private function removeCollisionNode(node:CollisionNode)
    {

        var link = mouseOverListeners.get(node);
        mouseOverListeners.remove(node);
        link.dissolve();
        
        link = mouseOutListeners.get(node);
        mouseOutListeners.remove(node);
        link.dissolve();
    }
}