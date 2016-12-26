package nanogarch.geom;
import hxmath.math.Vector2;

class Polygons
{
	public static function hex(orientation:HexOrientation,scale:Float)
	{
		var points = [];
        for (i in 0...6) {
            var angle = 2 * Math.PI * (2*i - (orientation==HexOrientation.POINTY_TOP? 1 : 0)) / 12;
            points.push(new Vector2(0.5 * scale * Math.cos(angle),
                                             0.5 * scale * Math.sin(angle)));
        }
        return points;
	}
}