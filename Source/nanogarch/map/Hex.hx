package nanogarch.map;
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
    public function equals(other:Hex):Bool {
        return x == other.x && y == other.y && z == other.z;
    }

    static public function add(a:Hex, b:Hex):Hex
    {
        return new Hex(a.x + b.x, a.y + b.y, a.z + b.z);
    }


    static public function subtract(a:Hex, b:Hex):Hex
    {
        return new Hex(a.x - b.x, a.y - b.y, a.z - b.z);
    }


    static public function scale(a:Hex, k:Int):Hex
    {
        return new Hex(a.x * k, a.y * k, a.z * k);
    }
    static public function neighbor(hex:Hex, direction:Int):Hex
    {
        return Hex.add(hex, Hex.direction(direction));
    }
    static public var directions:Array<Hex> = [new Hex(1, -1, 0), new Hex(1, 0, -1), new Hex(0, 1, -1), new Hex(-1, 1, 0), new Hex(-1, 0, 1), new Hex(0, -1, 1)];
    static public function direction(direction:Int):Hex
    {
        return Hex.directions[direction];
    }
    
}