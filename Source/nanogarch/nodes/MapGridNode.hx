package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.MapGridFrame;
import nanogarch.geom.Hex;
class MapGridNode extends Node<MapGridNode>
{
	public var gridFrame:MapGridFrame;
    public var x(get,null):Int;
    public var y(get,null):Int;
    public var z(get,null):Int;
    public var position(get,null):Hex;
    private inline function get_position():Hex
    {
    	return gridFrame.position; 
    }
    // private inline function get_hexFrame():Float
    // {
    //   return hexFrame;
    // }
    private inline function get_x():Int
    {
      return gridFrame.x;
    }
    private inline function get_y():Int
    {
      return gridFrame.y;
    }
    private inline function get_z():Int
    {
      return gridFrame.z;
    }
}