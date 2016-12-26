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
import nanogarch.nodes.RenderableNode;
import nanogarch.nodes.TransformNode;

// import nanogarch.nodes.RenderableMapNode;
import minject.Injector;
import nanogarch.GameConfig;
import hxmath.frames.Frame2;

// typedef HexFrameChangeListener = HexFrame -> Void;
using tink.CoreApi; 
class RenderSystem extends System
{
    @inject("GameDisplayObject") public var container:DisplayObjectContainer;
    @inject public var config:GameConfig;

    private var hexFrameChangeListeners:Map<TransformNode, CallbackLink>;

    private var renderableNodes:NodeList<RenderableNode>;
    private var transformNodes:NodeList<TransformNode>;
   
    public function new(){
        super();
        hexFrameChangeListeners = new Map<TransformNode, CallbackLink>();
    }

    override public function addToEngine(engine:Engine):Void
    {
  		//Center the view at (0,0)
  		var txMatrix:Matrix = new Matrix();
		txMatrix.translate(config.viewWidth*0.5,config.viewHeight*0.5);
		container.transform.matrix = txMatrix;
		
        transformNodes = engine.getNodeList(TransformNode);
        for (node in transformNodes)
            addTransformNode(node);
        transformNodes.nodeAdded.add(addTransformNode);
        transformNodes.nodeRemoved.add(removeTransformNode);

        renderableNodes = engine.getNodeList(RenderableNode);
        for (node in renderableNodes)
            addToDisplay(node);
        renderableNodes.nodeAdded.add(addToDisplay);
        renderableNodes.nodeRemoved.add(removeFromDisplay);
        trace("Added Render System");
    }
    private function addTransformNode(node:TransformNode)
    {
        var listener = onHexFrameChanged.bind(node);
        var link = node.hexFrame.frameChanged.handle(listener);
        hexFrameChangeListeners.set(node,link);
    }
    private function onHexFrameChanged(node:TransformNode,hexFrame:HexFrame)
    {
        trace("HEX FRAME CHANGED!");
    }
    private function removeTransformNode(node:TransformNode)
    {

        var link = hexFrameChangeListeners.get(node);
        hexFrameChangeListeners.remove(node);
        link.dissolve();
    }
    // private function transformHexFrame(node:HexFrameTransformNode)
    // {

    // }
    private function addToDisplay(node:RenderableNode):Void
    {
    	trace("Adding Render node to display List ");
    	container.addChild(node.displayObject);
    }

    private function removeFromDisplay(node:RenderableNode):Void
    {
        container.removeChild(node.displayObject);
    }

    override public function update(time:Float):Void
    {
        for (node in renderableNodes)
        {
            var displayObject:DisplayObject = node.displayObject;
            
            displayObject.x = node.x;
            displayObject.y = node.y;
            // displayObject.rotation = position.rotation * 180 / Math.PI;
        }
    }

    override public function removeFromEngine(engine:Engine):Void
    {
        renderableNodes = null;
         trace("Removed Render System");
    }
}
