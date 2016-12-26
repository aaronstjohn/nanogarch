package hex;

class FractionalHex
{
    public function new(q:Float, r:Float, s:Float)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }
    public var q:Float;
    public var r:Float;
    public var s:Float;

    static public function hexRound(h:FractionalHex):Hex
    {
        var q:Int = Math.round(h.q);
        var r:Int = Math.round(h.r);
        var s:Int = Math.round(h.s);
        var q_diff:Float = Math.abs(q - h.q);
        var r_diff:Float = Math.abs(r - h.r);
        var s_diff:Float = Math.abs(s - h.s);
        if (q_diff > r_diff && q_diff > s_diff)
        {
            q = -r - s;
        }
        else
            if (r_diff > s_diff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }
        return new Hex(q, r, s);
    }


    static public function hexLerp(a:FractionalHex, b:FractionalHex, t:Float):FractionalHex
    {
        return new FractionalHex(a.q * (1 - t) + b.q * t, a.r * (1 - t) + b.r * t, a.s * (1 - t) + b.s * t);
    }


    static public function hexLinedraw(a:Hex, b:Hex):Array<Hex>
    {
        var N:Int = Hex.distance(a, b);
        var a_nudge:FractionalHex = new FractionalHex(a.q + 0.000001, a.r + 0.000001, a.s - 0.000002);
        var b_nudge:FractionalHex = new FractionalHex(b.q + 0.000001, b.r + 0.000001, b.s - 0.000002);
        var results:Array<Hex> = [];
        var step:Float = 1.0 / Math.max(N, 1);
        for (i in 0...N + 1)
        {
            results.push(FractionalHex.hexRound(FractionalHex.hexLerp(a_nudge, b_nudge, step * i)));
        }
        return results;
    }

}