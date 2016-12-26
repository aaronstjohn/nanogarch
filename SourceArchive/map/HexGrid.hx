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
       public function cartesianToHex(p:ScreenCoordinate) {
        // NOTE: this is really the inverse matrix multiply. For the
        // matrix M defined in hexToCenter, get the inverse. It's
        // M**-1 in SymPy or M.I in NumPy.
        //
        // then [q, r] = (M**-1) * ([x, y] / size)
        //
        // Viewing these as matrix operations makes the transformation
        // from pixel to hex and hex to pixel easier to understand. I
        // used http://live.sympy.org/ to calculate them.
        var size = this.scale/2;
        p = p.scale(1/size);
        // NOTE: I calculate x,z cube coordinates and then calculate y
        // from those. That's because hex to pixel and pixel to hex
        // are defined in terms of axial coordinates, not cube
        // coordinates, and I am converting from axial to cube here.
        if (orientation) {
            var q = Math.sqrt(3)/3 * p.x + -1/3 * p.y;
            var r = 2/3 * p.y;
            return new FractionalHex(q, -q-r, r);
        } else {
            var q = 2/3 * p.x;
            var r = -1/3 * p.x + Math.sqrt(3)/3 * p.y;
            return new FractionalHex(q, -q-r, r);
        }
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