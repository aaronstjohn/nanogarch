package nanogarch.nodes;
import ash.core.Node;
import nanogarch.components.Frame;
import nanogarch.components.Collider;
class CollisionNode extends Node<CollisionNode>
{
	public var frame:Frame;
    public var collider:Collider;
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