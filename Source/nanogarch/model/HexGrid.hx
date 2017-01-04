package nanogarch.model;
import nanogarch.creators.ICreator;
import nanogarch.geom.Hex;
import haxe.ds.HashMap;
class HexGrid<T>
{
	private var contentMap:HashMap<Hex,T>;
	private var contentProvider:ICreator<T>;
	public function new(provider:ICreator<T>){
		contentMap =  new HashMap<Hex,T>();
		contentProvider=provider;
	}
	public function addHex(hex:Hex)
	{
		contentMap.set(hex,contentProvider.create());
	}
	public function addHexes(hexes:Array<Hex>)
	{
		for( hex in hexes)
			addHex(hex);
	}
	public function hexIterator():Iterator<Hex>
	{
		return contentMap.keys();
	}
	public function get(hex:Hex):T
	{
		// trace("Hex exists "+hex);
		// trace("Getting hex : "+hex.hashCode());
		if (contentMap.exists(hex))
			return contentMap.get(hex);
		// trace("CANT FIND HEX "+hex);
		return null;
	}
	
	static public function hexagonalShape(size:Int):Array<Hex> {
        var hexes = [];
        for (x in -size...size+1) {
            for (y in -size...size+1) {
                var z = -x-y;
                if (Math.abs(x) <= size && Math.abs(y) <= size && Math.abs(z) <= size) {
                    hexes.push(new Hex(x, y, z));
                }
            }
        }
        return hexes;
    }
}