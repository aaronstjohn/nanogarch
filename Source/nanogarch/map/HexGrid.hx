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
     public function polygonVertices() {
        var points = [];
        for (i in 0...6) {
            var angle = 2 * Math.PI * (2*i - (orientation? 1 : 0)) / 12;
            points.push(new ScreenCoordinate(0.5 * scale * Math.cos(angle),
                                             0.5 * scale * Math.sin(angle)));
        }
        return points;
    }

    public function hexToCenter(hex:Hex) {
        // NOTE: this is really a matrix multiply turning a 3-vector
        // into a 2-vector, and there's one matrix for each
        // orientation. In SymPy or NumPy you can write:
        //
        // M = Matrix( [ [ sqrt(3), sqrt(3)/2 ], [ 0, 3./2 ] ] )
        // or
        // M = Matrix( [ [ 3./2, 0 ], [ sqrt(3)/2, sqrt(3) ] ] )
        //
        // then [x, y] = size * M * [q, r]
        var s:ScreenCoordinate;
        var size = this.scale/2;
        // NOTE: I am ignoring cube.y. Why? Because I'm implicitly
        // converting cube coordinates to axial coordinates here. I
        // have hex to pixel and pixel to hex defined for axial
        // coordinates, *not* cube coordinates.
        if (orientation) {
            s = new ScreenCoordinate(Math.sqrt(3) * hex.x
                                     + Math.sqrt(3)/2 * hex.z,
                                     1.5 * hex.z);
        } else {
            s = new ScreenCoordinate(1.5 * hex.x,
                                     Math.sqrt(3)/2 * hex.x
                                     + Math.sqrt(3) * hex.z);
        }
        return s.scale(size);
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