package nanogarch.geom;
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