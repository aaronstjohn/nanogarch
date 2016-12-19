package nanogarch.map;
class FractionalHex
{
    public function new(x:Float, y:Float, z:Float)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public var x:Float;
    public var y:Float;
    public var z:Float;

    static public function hexRound(h:FractionalHex):Hex
    {
        var rx:Int = Math.round(h.x);
        var ry:Int = Math.round(h.y);
        var rz:Int = Math.round(h.z);
        var x_diff:Float = Math.abs(rx - h.x);
        var y_diff:Float = Math.abs(ry - h.y);
        var z_diff:Float = Math.abs(rz - h.z);
        if (x_diff > y_diff && x_diff > z_diff)
            {
                rx = -ry - rz;
            }
        else
            if (y_diff > z_diff)
                {
                    ry = -rx - rz;
                }
            else
                {
                    rz = -rx - ry;
                }
        return new Hex(rx, ry, rz);
    }


    static public function hexLerp(a:Hex, b:Hex, t:Float):FractionalHex
    {
        return new FractionalHex(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
    }

    static public function hexLinedraw(a:Hex, b:Hex):Array<Hex>
    {
        var N:Int = Hex.distance(a, b);
        var results:Array<Hex> = [];
        for (i in 0...N + 1)
            {
                results.push(FractionalHex.hexRound(FractionalHex.hexLerp(a, b, 1.0 / Math.max(1,N) * i)));
            }
        return results;
    }


    public function v() {
        return [x, y, z];
    }
    
    public function toString() {
        return "#{" + v().join(",") + "}";
    }

}