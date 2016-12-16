package nanogarch.nodes;
import nanogarch.components.MapPosition;
import nanogarch.map.Hex;
import ash.core.Node;

class MapPositionNode extends Node<MapPositionNode>
{
	public var mapPosition:MapPosition;

	public var position(get_position,never):Hex;
   	
   	private inline function get_position():Hex
    {
        return mapPosition.position;
    }
}
