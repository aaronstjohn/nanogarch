package ;

import hex.*;
class HexTests
{
    public function new()
    {
    }

    static public function equalHex(name:String, a:Hex, b:Hex):Void
    {
        if (!(a.q == b.q && a.s == b.s && a.r == b.r))
        {
            HexTests.complain(name);
        }
    }


    static public function equalOffsetcoord(name:String, a:OffsetCoord, b:OffsetCoord):Void
    {
        if (!(a.col == b.col && a.row == b.row))
        {
            HexTests.complain(name);
        }
    }


    static public function equalInt(name:String, a:Int, b:Int):Void
    {
        if (!(a == b))
        {
            HexTests.complain(name);
        }
    }


    static public function equalHexArray(name:String, a:Array<Hex>, b:Array<Hex>):Void
    {
        HexTests.equalInt(name, a.length, b.length);
        for (i in 0...a.length)
        {
            HexTests.equalHex(name, a[i], b[i]);
        }
    }


    static public function testHexArithmetic():Void
    {
        HexTests.equalHex("hex_add", new Hex(4, -10, 6), Hex.add(new Hex(1, -3, 2), new Hex(3, -7, 4)));
        HexTests.equalHex("hex_subtract", new Hex(-2, 4, -2), Hex.subtract(new Hex(1, -3, 2), new Hex(3, -7, 4)));
    }


    static public function testHexDirection():Void
    {
        HexTests.equalHex("hex_direction", new Hex(0, -1, 1), Hex.direction(2));
    }


    static public function testHexNeighbor():Void
    {
        HexTests.equalHex("hex_neighbor", new Hex(1, -3, 2), Hex.neighbor(new Hex(1, -2, 1), 2));
    }


    static public function testHexDiagonal():Void
    {
        HexTests.equalHex("hex_diagonal", new Hex(-1, -1, 2), Hex.diagonalNeighbor(new Hex(1, -2, 1), 3));
    }


    static public function testHexDistance():Void
    {
        HexTests.equalInt("hex_distance", 7, Hex.distance(new Hex(3, -7, 4), new Hex(0, 0, 0)));
    }


    static public function testHexRound():Void
    {
        var a:FractionalHex = new FractionalHex(0, 0, 0);
        var b:FractionalHex = new FractionalHex(1, -1, 0);
        var c:FractionalHex = new FractionalHex(0, -1, 1);
        HexTests.equalHex("hex_round 1", new Hex(5, -10, 5), FractionalHex.hexRound(FractionalHex.hexLerp(new FractionalHex(0, 0, 0), new FractionalHex(10, -20, 10), 0.5)));
        HexTests.equalHex("hex_round 2", FractionalHex.hexRound(a), FractionalHex.hexRound(FractionalHex.hexLerp(a, b, 0.499)));
        HexTests.equalHex("hex_round 3", FractionalHex.hexRound(b), FractionalHex.hexRound(FractionalHex.hexLerp(a, b, 0.501)));
        HexTests.equalHex("hex_round 4", FractionalHex.hexRound(a), FractionalHex.hexRound(new FractionalHex(a.q * 0.4 + b.q * 0.3 + c.q * 0.3, a.r * 0.4 + b.r * 0.3 + c.r * 0.3, a.s * 0.4 + b.s * 0.3 + c.s * 0.3)));
        HexTests.equalHex("hex_round 5", FractionalHex.hexRound(c), FractionalHex.hexRound(new FractionalHex(a.q * 0.3 + b.q * 0.3 + c.q * 0.4, a.r * 0.3 + b.r * 0.3 + c.r * 0.4, a.s * 0.3 + b.s * 0.3 + c.s * 0.4)));
    }


    static public function testHexLinedraw():Void
    {
        HexTests.equalHexArray("hex_linedraw", [new Hex(0, 0, 0), new Hex(0, -1, 1), new Hex(0, -2, 2), new Hex(1, -3, 2), new Hex(1, -4, 3), new Hex(1, -5, 4)], FractionalHex.hexLinedraw(new Hex(0, 0, 0), new Hex(1, -5, 4)));
    }


    static public function testLayout():Void
    {
        var h:Hex = new Hex(3, 4, -7);
        var flat:Layout = new Layout(Layout.flat, new Point(10, 15), new Point(35, 71));
        HexTests.equalHex("layout", h, FractionalHex.hexRound(Layout.pixelToHex(flat, Layout.hexToPixel(flat, h))));
        var pointy:Layout = new Layout(Layout.pointy, new Point(10, 15), new Point(35, 71));
        HexTests.equalHex("layout", h, FractionalHex.hexRound(Layout.pixelToHex(pointy, Layout.hexToPixel(pointy, h))));
    }


    static public function testConversionRoundtrip():Void
    {
        var a:Hex = new Hex(3, 4, -7);
        var b:OffsetCoord = new OffsetCoord(1, -3);
        HexTests.equalHex("conversion_roundtrip even-q", a, OffsetCoord.qoffsetToCube(OffsetCoord.EVEN, OffsetCoord.qoffsetFromCube(OffsetCoord.EVEN, a)));
        HexTests.equalOffsetcoord("conversion_roundtrip even-q", b, OffsetCoord.qoffsetFromCube(OffsetCoord.EVEN, OffsetCoord.qoffsetToCube(OffsetCoord.EVEN, b)));
        HexTests.equalHex("conversion_roundtrip odd-q", a, OffsetCoord.qoffsetToCube(OffsetCoord.ODD, OffsetCoord.qoffsetFromCube(OffsetCoord.ODD, a)));
        HexTests.equalOffsetcoord("conversion_roundtrip odd-q", b, OffsetCoord.qoffsetFromCube(OffsetCoord.ODD, OffsetCoord.qoffsetToCube(OffsetCoord.ODD, b)));
        HexTests.equalHex("conversion_roundtrip even-r", a, OffsetCoord.roffsetToCube(OffsetCoord.EVEN, OffsetCoord.roffsetFromCube(OffsetCoord.EVEN, a)));
        HexTests.equalOffsetcoord("conversion_roundtrip even-r", b, OffsetCoord.roffsetFromCube(OffsetCoord.EVEN, OffsetCoord.roffsetToCube(OffsetCoord.EVEN, b)));
        HexTests.equalHex("conversion_roundtrip odd-r", a, OffsetCoord.roffsetToCube(OffsetCoord.ODD, OffsetCoord.roffsetFromCube(OffsetCoord.ODD, a)));
        HexTests.equalOffsetcoord("conversion_roundtrip odd-r", b, OffsetCoord.roffsetFromCube(OffsetCoord.ODD, OffsetCoord.roffsetToCube(OffsetCoord.ODD, b)));
    }


    static public function testOffsetFromCube():Void
    {
        HexTests.equalOffsetcoord("offset_from_cube even-q", new OffsetCoord(1, 3), OffsetCoord.qoffsetFromCube(OffsetCoord.EVEN, new Hex(1, 2, -3)));
        HexTests.equalOffsetcoord("offset_from_cube odd-q", new OffsetCoord(1, 2), OffsetCoord.qoffsetFromCube(OffsetCoord.ODD, new Hex(1, 2, -3)));
    }


    static public function testOffsetToCube():Void
    {
        HexTests.equalHex("offset_to_cube even-", new Hex(1, 2, -3), OffsetCoord.qoffsetToCube(OffsetCoord.EVEN, new OffsetCoord(1, 3)));
        HexTests.equalHex("offset_to_cube odd-q", new Hex(1, 2, -3), OffsetCoord.qoffsetToCube(OffsetCoord.ODD, new OffsetCoord(1, 2)));
    }


    static public function testAll():Void
    {
        HexTests.testHexArithmetic();
        HexTests.testHexDirection();
        HexTests.testHexNeighbor();
        HexTests.testHexDiagonal();
        HexTests.testHexDistance();
        HexTests.testHexRound();
        HexTests.testHexLinedraw();
        HexTests.testLayout();
        HexTests.testConversionRoundtrip();
        HexTests.testOffsetFromCube();
        HexTests.testOffsetToCube();
    }


    static public function main():Void
    {
        trace("RUNNING TESTS!");
        HexTests.testAll();
        trace("TESTS COMPLETE !");
    }


    static public function complain(name:String):Void
    {
        trace("FAIL ", name);
    }

}