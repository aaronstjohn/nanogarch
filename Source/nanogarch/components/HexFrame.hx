package nanogarch.components;
import nanogarch.geom.Hex;
import nanogarch.geom.HexDirection;
import nanogarch.geom.HexOrientation;

using tink.CoreApi;
class HexFrame
{
	var offset:Hex;
	public var position(get_position,null):Hex;
	public var x(get_x,null):Int;
	public var y(get_y,null):Int;
	public var z(get_z,null):Int;
	public var direction(default,null):HexDirection;

	public function new(x:Int=0, y:Int=0, z:Int=0,  direction:Int =0)
	{
		this.offset = new Hex(x,y,z);
		this.direction = HexDirection.directions[direction];
		frameChanged= frameChangedTrigger = Signal.trigger();

	}
	public function set_offset(x:Int,y:Int,z:Int)
	{
		// frame.offset.set(x,y);
		offset.set(x,y,z); 
		frameChangedTrigger.trigger(this);
	}
	private inline function get_x():Int
	{
	  return offset.x;
	}
	private inline function get_y():Int
	{
	  return offset.y;
	}
	private inline function get_z():Int
	{
	  return offset.z;
	}
	private inline function get_position():Hex
	{
	  return offset;
	}

	public var frameChanged(default, null):Signal<HexFrame>;
	var frameChangedTrigger:SignalTrigger<HexFrame>;
}