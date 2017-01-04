package nanogarch.components;
import nanogarch.geom.Hex;
import hxmath.math.Vector2;
import nanogarch.geom.HexDirection;
import nanogarch.geom.HexOrientation;

using tink.CoreApi;
class MapGridFrame
{
	
	// var offset:Hex;
	public var position(get,null):Hex;
	// public var cartesian(get,null):Vector2;
	public var x(get_x,null):Int;
	public var y(get_y,null):Int;
	public var z(get_z,null):Int;
	public var direction(default,null):HexDirection;

	public function new(x:Int=0, y:Int=0, z:Int=0,  direction:Int =0)
	{
		this.position = new Hex(x,y,z);
		// this.cartesian = this.position.toCartesian(config.worldHexScale,config.worldHexOrientation);
		
		this.direction = HexDirection.directions[direction];
		frameChanged= frameChangedTrigger = Signal.trigger();
		moveRequested= moveRequestedTrigger = Signal.trigger();

	}
	public function set(hex:Hex)
	{
		position = hex;
	}
	
	public function offset(x:Int,y:Int,z:Int)
	{
		// frame.offset.set(x,y);
		position.set(x,y,z); 
		frameChangedTrigger.trigger(this);
	}
	private inline function get_x():Int
	{
	  return position.x;
	}
	private inline function get_y():Int
	{
	  return position.y;
	}
	private inline function get_z():Int
	{
	  return position.z;
	}
	private inline function get_position():Hex
	{
	  return position;
	}
	public function requestMove(direction:HexDirection):Void
    {
        moveRequestedTrigger.trigger(direction);
    }
	public var frameChanged(default, null):Signal<MapGridFrame>;
	var frameChangedTrigger:SignalTrigger<MapGridFrame>;
	public var moveRequested(default, null):Signal<HexDirection>;
	var moveRequestedTrigger:SignalTrigger<HexDirection>;
}