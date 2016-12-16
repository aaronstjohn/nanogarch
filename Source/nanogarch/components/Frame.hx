package nanogarch.components;
// import hex.Cube;
import hxmath.frames.Frame2;
import hxmath.math.Vector2;
class Frame
{
	public var frame(default,null):Frame2;

	public function new(x:Float=0.0, y:Float=0.0, angle:Float = 0.0)
	{
		this.frame = new Frame2(new Vector2(x,y),angle);
	}
}