package nanogarch.map;
class HexDirection
{
	public var direction(get_direction, never):Hex;
	public var directionIndex(default,null):Int;

	public function new(dirIndx:Int=0)
	{
		directionIndex = dirIndx;
	}
	function get_direction():Hex
	{	
		return Hex.directions[directionIndex];
	}
}