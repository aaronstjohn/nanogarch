package nanogarch.components;
// import hex.Cube;
import hxmath.frames.Frame2;
import hxmath.math.Vector2;
using tink.CoreApi;
class Frame
{
	var frame:Frame2;
	public var position(get_position,null):Vector2;
	public var x(get_x,null):Float;
	public var y(get_y,null):Float;
	public var angle(get_angle,null):Float;

	public function new(x:Float=0.0, y:Float=0.0, angle:Float = 0.0)
	{
		this.frame = new Frame2(new Vector2(x,y),angle);
		frameChanged= frameChangedTrigger = Signal.trigger();

	}
	public function offset(x:Float,y:Float)
	{
		frame.offset.set(x,y);
		frameChangedTrigger.trigger(this);
	}
	private inline function get_x():Float
	{
	  return frame.offset.x;
	}
	private inline function get_y():Float
	{
	  return frame.offset.y;
	}
	private inline function get_angle():Float
	{
	  return frame.angleDegrees;
	}
	private inline function get_position():Vector2
	{
	  return frame.offset;
	}

	public var frameChanged(default, null):Signal<Frame>;
	var frameChangedTrigger:SignalTrigger<Frame>;

}