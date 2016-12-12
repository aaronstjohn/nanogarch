package hex;


class Layout
{
    public function new(orientation:Orientation, size:Point, origin:Point)
    {
        this.orientation = orientation;
        this.size = size;
        this.origin = origin;
    }
    public var orientation:Orientation;
    public var size:Point;
    public var origin:Point;
    static public var pointy:Orientation = new Orientation(Math.sqrt(3.0), Math.sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
    static public var flat:Orientation = new Orientation(3.0 / 2.0, 0.0, Math.sqrt(3.0) / 2.0, Math.sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.sqrt(3.0) / 3.0, 0.0);

    static public function hexToPixel(layout:Layout, h:Hex):Point
    {
        var M:Orientation = layout.orientation;
        var size:Point = layout.size;
        var origin:Point = layout.origin;
        var x:Float = (M.f0 * h.q + M.f1 * h.r) * size.x;
        var y:Float = (M.f2 * h.q + M.f3 * h.r) * size.y;
        return new Point(x + origin.x, y + origin.y);
    }


    static public function pixelToHex(layout:Layout, p:Point):FractionalHex
    {
        var M:Orientation = layout.orientation;
        var size:Point = layout.size;
        var origin:Point = layout.origin;
        var pt:Point = new Point((p.x - origin.x) / size.x, (p.y - origin.y) / size.y);
        var q:Float = M.b0 * pt.x + M.b1 * pt.y;
        var r:Float = M.b2 * pt.x + M.b3 * pt.y;
        return new FractionalHex(q, r, -q - r);
    }


    static public function hexCornerOffset(layout:Layout, corner:Int):Point
    {
        var M:Orientation = layout.orientation;
        var size:Point = layout.size;
        var angle:Float = 2.0 * Math.PI * (M.start_angle - corner) / 6;
        return new Point(size.x * Math.cos(angle), size.y * Math.sin(angle));
    }


    static public function polygonCorners(layout:Layout, h:Hex):Array<Point>
    {
        var corners:Array<Point> = [];
        var center:Point = Layout.hexToPixel(layout, h);
        for (i in 0...6)
        {
            var offset:Point = Layout.hexCornerOffset(layout, i);
            corners.push(new Point(center.x + offset.x, center.y + offset.y));
        }
        return corners;
    }

}
