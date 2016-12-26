// Generated code -- http://www.redblobgames.com/grids/hexagons/



package hex;




class Hex
{
    public function new(q:Int, r:Int, s:Int)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }
    public var q:Int;
    public var r:Int;
    public var s:Int;

    static public function add(a:Hex, b:Hex):Hex
    {
        return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
    }


    static public function subtract(a:Hex, b:Hex):Hex
    {
        return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
    }


    static public function scale(a:Hex, k:Int):Hex
    {
        return new Hex(a.q * k, a.r * k, a.s * k);
    }

    static public var directions:Array<Hex> = [new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1)];

    static public function direction(direction:Int):Hex
    {
        return Hex.directions[direction];
    }


    static public function neighbor(hex:Hex, direction:Int):Hex
    {
        return Hex.add(hex, Hex.direction(direction));
    }

    static public var diagonals:Array<Hex> = [new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2)];

    static public function diagonalNeighbor(hex:Hex, direction:Int):Hex
    {
        return Hex.add(hex, Hex.diagonals[direction]);
    }


    static public function length(hex:Hex):Int
    {
        return Std.int((Math.abs(hex.q) + Math.abs(hex.r) + Math.abs(hex.s)) / 2);
    }


    static public function distance(a:Hex, b:Hex):Int
    {
        return Hex.length(Hex.subtract(a, b));
    }

}






