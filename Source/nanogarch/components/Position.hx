package nanogarch.components;
import hex.Cube;
class Position
{
	public var cube(default,null):Cube;

	public function new(x:Int, y:Int, z:Int)
	{
		this.cube = new Cube(x,y,z);
	}
}