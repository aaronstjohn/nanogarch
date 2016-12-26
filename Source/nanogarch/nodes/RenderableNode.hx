package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.Frame;
import hxmath.frames.Frame2;
import openfl.display.DisplayObject;



import nanogarch.components.Display;


/**
 * Node for rendering. Note that here it demonstrates how nodes work:
 *
 * component system treats all variables (both public and private) as links to required components
 * and sets their values on node initialization, while properties and functions are ignored completely
 * and can be used to make node API more useful
 **/
class RenderableNode extends Node<RenderableNode>
{
    public var frame:Frame;
    private var display:Display;

    public var displayObject(get_displayObject, never):DisplayObject;
    
    private inline function get_displayObject():DisplayObject
    {
        return display.displayObject;
    }
    public var x(get_x,null):Float;
    public var y(get_y,null):Float;
    private inline function get_x():Float
    {
      return frame.x;
    }
    private inline function get_y():Float
    {
      return frame.y;
    }
}
