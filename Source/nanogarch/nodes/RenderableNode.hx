package nanogarch.nodes;

import openfl.display.DisplayObject;

import ash.core.Node;

import nanogarch.components.Display;
import nanogarch.components.Frame;
import hxmath.frames.Frame2;

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
    public var frameObject(get_frameObject,never):Frame2;
    private inline function get_frameObject():Frame2
    {
        return frame.frame;
    }
    private inline function get_displayObject():DisplayObject
    {
        return display.displayObject;
    }
}
