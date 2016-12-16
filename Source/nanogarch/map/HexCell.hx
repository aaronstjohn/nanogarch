package nanogarch.map;
import ash.core.Entity;

class HexCell
{
	public function new(pos:Hex)
	{
		this.position = pos;
	}
	public var position:Hex;
	public var neighbors:Map<Hex,HexCell>;
	public var entities:Array<Entity>;

}