
package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.Frame;
import nanogarch.components.HexFrame;
import hxmath.frames.Frame2;
class TransformNode extends Node<TransformNode>
{
    public var frame:Frame;
    public var hexFrame:HexFrame;

    // public var displayObject(get_displayObject, never):DisplayObject;
    // public var frameObject(get_frameObject,never):Frame2;
    // private inline function get_frameObject():Frame2
    // {
    //     return frame.frame;
    // }
    // private inline function get_displayObject():DisplayObject
    // {
    //     return display.displayObject;
    // }
}