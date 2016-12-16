package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.Frame;
import nanogarch.components.MapPosition;
import nanogarch.components.Display;
import openfl.display.DisplayObject;
import nanogarch.map.Hex;
import hxmath.frames.Frame2;
class RenderableMapNode extends Node<RenderableMapNode>
{
    public var frame:Frame;
    public var mapPosition:MapPosition;

    private var display:Display;

    public var displayObject(get_displayObject, never):DisplayObject;
    public var frameObject(get_frameObject,never):Frame2;
    public var position(get_position,never):Hex;

    private inline function get_frameObject():Frame2
    {
        return frame.frame;
    }
    private inline function get_displayObject():DisplayObject
    {
        return display.displayObject;
    }
	private inline function get_position():Hex
    {
        return mapPosition.position;
    }
}