package nanogarch.systems;

import openfl.display.DisplayObject;
import openfl.display.DisplayObjectContainer;
import openfl.geom.Matrix;

import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;

import nanogarch.components.Display;
import nanogarch.components.Frame;
import nanogarch.nodes.RenderableNode;

// import nanogarch.nodes.RenderableMapNode;
import minject.Injector;
import nanogarch.GameConfig;
import hxmath.frames.Frame2;

class RenderSystem extends System
{
    @inject("GameDisplayObject") public var container:DisplayObjectContainer;
    @inject public var config:GameConfig;
    private var renderableNodes:NodeList<RenderableNode>;
    // private var renderableMapNodes:NodeList<RenderableMapNode>;

    public function new()
    
    {
        super();
    }

    override public function addToEngine(engine:Engine):Void
    {
  		//Center the view at (0,0)
  		var txMatrix:Matrix = new Matrix();
		txMatrix.translate(config.viewWidth*0.5,config.viewHeight*0.5);
		container.transform.matrix = txMatrix;
		
        renderableNodes = engine.getNodeList(RenderableNode);
        for (node in renderableNodes)
            addToDisplay(node);
        renderableNodes.nodeAdded.add(addToDisplay);
        renderableNodes.nodeRemoved.add(removeFromDisplay);
        trace("Added Render System");
    }

    private function addToDisplay(node:RenderableNode):Void
    {
    	trace("Adding Render node to display List ");
    	trace("Container :" + container);
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
            var frame:Frame2 = node.frameObject;

            displayObject.x = frame.offset.x;
            displayObject.y = frame.offset.y;
            // displayObject.rotation = position.rotation * 180 / Math.PI;
        }
    }

    override public function removeFromEngine(engine:Engine):Void
    {
        renderableNodes = null;
         trace("Removed Render System");
    }
}
