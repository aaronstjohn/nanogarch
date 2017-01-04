package nanogarch.geom;
import hxmath.math.Vector2;
class Hex
{
    public var x:Int;
    public var y:Int;
    public var z:Int;
	public function new(x:Int, y:Int, z:Int)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public function set(x:Int, y:Int, z:Int)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public function equals(other:Hex):Bool {
        return x == other.x && y == other.y && z == other.z;
    }
    public function hashCode():Int
    {
        //http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
        var hash:Int = 17;
        hash = hash*486187739 + x;
        hash = hash*486187739 + y;
        hash = hash*486187739 + z;
        return hash;
        
    }
    
    public function toString()
    {
        return '($x,$y,$z)';
    }
    static public function add(a:Hex, b:Hex):Hex
    {
        return new Hex(a.x + b.x, a.y + b.y, a.z + b.z);
    }


    static public function subtract(a:Hex, b:Hex):Hex
    {
        return new Hex(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    static public function distance(a:Hex, b:Hex):Int
    {
        return Hex.length(Hex.subtract(a, b));
    }
    static public function length(hex:Hex):Int
    {
        return Std.int((Math.abs(hex.x) + Math.abs(hex.y) + Math.abs(hex.z)) / 2);
    }
    static public function scale(a:Hex, k:Int):Hex
    {
        return new Hex(a.x * k, a.y * k, a.z * k);
    }
    public function toCartesian(scale:Float,orientation:HexOrientation) :Vector2
    {
        // NOTE: this is really a matrix multiply turning a 3-vector
        // into a 2-vector, and there's one matrix for each
        // orientation. In SymPy or NumPy you can write:
        //
        // M = Matrix( [ [ sqrt(3), sqrt(3)/2 ], [ 0, 3./2 ] ] )
        // or
        // M = Matrix( [ [ 3./2, 0 ], [ sqrt(3)/2, sqrt(3) ] ] )
        //
        // then [x, y] = size * M * [q, r]
        var s:Vector2;
        var size = scale/2;
        // NOTE: I am ignoring cube.y. Why? Because I'm implicitly
        // converting cube coordinates to axial coordinates here. I
        // have hex to pixel and pixel to hex defined for axial
        // coordinates, *not* cube coordinates.
        if (orientation==HexOrientation.POINTY_TOP) {
            s = new Vector2(Math.sqrt(3) * x
                                     + Math.sqrt(3)/2 * z,
                                     1.5 * z);
        } else {
            s = new Vector2(1.5 * x,
                                     Math.sqrt(3)/2 * x
                                     + Math.sqrt(3) * z);
        }
        return s*size;
    }
    // static public function neighbor(hex:Hex, direction:Int):Hex
    // {
    //     return Hex.add(hex, Hex.direction(direction));
    // }
    // static public var directions:Array<Hex> = [new Hex(1, -1, 0), new Hex(1, 0, -1), new Hex(0, 1, -1), new Hex(-1, 1, 0), new Hex(-1, 0, 1), new Hex(0, -1, 1)];
    // static public function direction(direction:Int):Hex
    // {
    //     return Hex.directions[direction];
    // }
    
}