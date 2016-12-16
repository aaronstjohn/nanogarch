package nanogarch.map;
import nanogarch.components.MapObject;

class HexCell
{
	public function new(pos:Hex)
	{
		this.position = pos;
	}
	var position:Hex;
	var neighbors:Std.Map<Direction,HexCell>;
	var units:Array<Entity>;

}