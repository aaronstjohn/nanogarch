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
	private var collisionNodes:NodeList<CollisionNode>;
	public function new(){
		super();
		mouseOverListeners = new Map<CollisionNode, CallbackLink>();
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
    	node.collider.collisionShape.x = node.x+50;
    	node.collider.collisionShape.y= node.y+50;
    	
        var listener = onMouseOverCollision.bind(node);
        var link = node.collider.mouseOver.handle(listener);
        mouseOverListeners.set(node,link);
    
    }
    private function onMouseOverCollision(node:CollisionNode,event:MouseEvent)
    {
        trace("Mouse Over!");
    }
    // private function onFrameChanged(node:CollisionNode,frame:Frame)
    // {

    // }
    private function removeCollisionNode(node:CollisionNode)
    {

        // var link = hexFrameChangeListeners.get(node);
        // hexFrameChangeListeners.remove(node);
        // link.dissolve();
    }
}