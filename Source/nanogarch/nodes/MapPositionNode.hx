package nanogarch.nodes;
import nanogarch.components.MapPosition;
import nanogarch.components.Frame;
import hxmath.frames.Frame2;
import nanogarch.map.Hex;
import ash.core.Node;

class MapPositionNode extends Node<MapPositionNode>
{
	public var mapPosition:MapPosition;
	public var frame:Frame;
	public var position(get_position,never):Hex;
   	public var frameObject(get_frameObject,never):Frame2;
    private inline function get_frameObject():Frame2
    {
        return frame.frame;
    }
   	private inline function get_position():Hex
    {
        return mapPosition.position;
    }
}
