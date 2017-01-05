package nanogarch.graphics;
import openfl.display.Sprite;
import util.GraphicsExt;
import hxmath.math.Vector2;
import nanogarch.StyleConfig;
import nanogarch.Styles;

class ShapeView extends Sprite
{
	var style:Styles.Shape;
	var shape:Array<Vector2>;
	
	public function new(){super();}
	public function initialize(shape:Array<Vector2>,style:Styles.Shape)
	{
		this.shape = shape;
		this.style = style;
		redraw();
		return this;
	}
	public function redraw()
	{
		GraphicsExt.drawPoly(graphics,shape,style.thickness,style.fill,style.outline);
    	this.mouseEnabled = true;
	}
}