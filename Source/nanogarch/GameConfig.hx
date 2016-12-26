package nanogarch;
import nanogarch.geom.HexOrientation;
class GameConfig
{
    public var viewWidth:Float;
    public var viewHeight:Float;
    public var worldHexOrientation:HexOrientation;
    public var worldHexScale:Float;

    public function new()
    {
    	worldHexOrientation = HexOrientation.POINTY_TOP;
    	worldHexScale = 75.0;
    }
}
