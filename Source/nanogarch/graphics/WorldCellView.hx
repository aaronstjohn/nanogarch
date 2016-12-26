package nanogarch.graphics;
import openfl.display.Sprite;
import util.GraphicsExt;
import hxmath.math.Vector2;
import nanogarch.StyleConfig;
class WorldCellView extends Sprite
{
	@inject public var styleConfig:StyleConfig;

	public var fillColor:UInt;
	public var outlineColor:UInt;
	public var lineThickness:Float;

	public function new()
	{
		super();
		
	
	}
	
    public function initialize(poly:Array<Vector2>):WorldCellView
    {
    	fillColor = styleConfig.defaultColliderFillColor;
		outlineColor = styleConfig.defaultColliderLineColor;
		lineThickness = styleConfig.defaultColliderLineThickness;
    	GraphicsExt.drawPoly(graphics,poly,lineThickness,fillColor,outlineColor);
    	this.mouseEnabled = false;
    	return this;
    }
}