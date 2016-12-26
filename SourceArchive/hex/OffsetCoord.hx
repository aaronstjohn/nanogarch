package hex;


class OffsetCoord
{
    public function new(col:Int, row:Int)
    {
        this.col = col;
        this.row = row;
    }
    public var col:Int;
    public var row:Int;
    static public var EVEN:Int = 1;
    static public var ODD:Int = -1;

    static public function qoffsetFromCube(offset:Int, h:Hex):OffsetCoord
    {
        var col:Int = h.q;
        var row:Int = h.r + Std.int((h.q + offset * (h.q & 1)) / 2);
        return new OffsetCoord(col, row);
    }


    static public function qoffsetToCube(offset:Int, h:OffsetCoord):Hex
    {
        var q:Int = h.col;
        var r:Int = h.row - Std.int((h.col + offset * (h.col & 1)) / 2);
        var s:Int = -q - r;
        return new Hex(q, r, s);
    }


    static public function roffsetFromCube(offset:Int, h:Hex):OffsetCoord
    {
        var col:Int = h.q + Std.int((h.r + offset * (h.r & 1)) / 2);
        var row:Int = h.r;
        return new OffsetCoord(col, row);
    }


    static public function roffsetToCube(offset:Int, h:OffsetCoord):Hex
    {
        var q:Int = h.col - Std.int((h.row + offset * (h.row & 1)) / 2);
        var r:Int = h.row;
        var s:Int = -q - r;
        return new Hex(q, r, s);
    }

}