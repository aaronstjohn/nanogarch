package nanogarch.map;
import ash.core.Entity;

class HexCell
{
	public function new(pos:Hex)
	{
		this.position = pos;
		entities = new Array<Entity>();
		neighbors = new Map<HexDirection,HexCell>();
	}
	public var position:Hex;
	public var neighbors:Map<HexDirection,HexCell>;
	public var entities:Array<Entity>;

}