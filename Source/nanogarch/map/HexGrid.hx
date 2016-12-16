package nanogarch.map;
class HexGrid
{
	public var hexes:Array<Hex>;
	public var orientation:Bool;
    public var scale:Float;

	public function new(scale, orientation, shape) {
		this.scale = scale;
        this.orientation = orientation;
        this.hexes = shape;
        
    }
	static public function hexagonalShape(size:Int):Array<Cube> {
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