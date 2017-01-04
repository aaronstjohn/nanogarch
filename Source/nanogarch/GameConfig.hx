package nanogarch;
import nanogarch.geom.HexOrientation;
import hxmath.math.Vector2;
import nanogarch.geom.Polygons;
class GameConfig
{
    public var viewWidth:Float;
    public var viewHeight:Float;
    public var worldHexOrientation:HexOrientation;
    public var worldHexScale:Float;
    public var referenceHexPolygon:Array<Vector2>;
    public function new()
    {
    	worldHexOrientation = HexOrientation.POINTY_TOP;
    	worldHexScale = 75.0;
    	referenceHexPolygon = Polygons.hex(worldHexOrientation,worldHexScale);
    }
}
