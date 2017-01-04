
class HexGrid<T>
{
	private var contentMap:HashMap<Hex,T>;
	public function new()
	{
		contentMap =  new HashMap<Hex,T>();
	}
}