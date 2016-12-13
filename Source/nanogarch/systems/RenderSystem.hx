package nanogarch.systems;

import openfl.display.DisplayObject;
import openfl.display.DisplayObjectContainer;
import openfl.geom.Matrix;

import ash.core.Engine;
import ash.core.NodeList;
import ash.core.System;

import nanogarch.components.Display;
import nanogarch.components.Position;
import nanogarch.nodes.RenderNode;
import minject.Injector;
import nanogarch.GameConfig;

class RenderSystem extends System
{
    @inject("GameDisplayObject") public var container:DisplayObjectContainer;
    @inject public var config:GameConfig;
    private var nodes:NodeList<RenderNode>;

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
		
        nodes = engine.getNodeList(RenderNode);
        for (node in nodes)
            addToDisplay(node);
        nodes.nodeAdded.add(addToDisplay);
        nodes.nodeRemoved.add(removeFromDisplay);
        trace("Added Render System");
    }

    private function addToDisplay(node:RenderNode):Void
    {
    	trace("Adding Render node to display List ");
    	trace("Container :" + container);
        container.addChild(node.displayObject);
    }

    private function removeFromDisplay(node:RenderNode):Void
    {
        container.removeChild(node.displayObject);
    }

    override public function update(time:Float):Void
    {
        for (node in nodes)
        {
            var displayObject:DisplayObject = node.displayObject;
            var position:Position = node.position;

            displayObject.x = position.cube.x;
            displayObject.y = position.cube.y;
            // displayObject.rotation = position.rotation * 180 / Math.PI;
        }
    }

    override public function removeFromEngine(engine:Engine):Void
    {
        nodes = null;
         trace("Removed Render System");
    }
}
