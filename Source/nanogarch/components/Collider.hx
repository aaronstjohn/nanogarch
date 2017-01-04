package nanogarch.components;
import hxmath.frames.Frame2;
import hxmath.math.Vector2;
import openfl.display.Shape;
import openfl.display.Sprite;
import util.GraphicsExt;
import openfl.events.Event;
import openfl.events.MouseEvent;
import openfl.display.DisplayObjectContainer;
import openfl.events.IEventDispatcher;
import util.SignalUtil;
using tink.CoreApi; 
class Collider
{
	@inject public var styleConfig:StyleConfig;

	public var collisionPoly:Array<Vector2>;
	public var collisionShape:Sprite;
	public var collisionFrame:Frame2;


	public var visible:Bool;
	public var fillColor:UInt;
	public var outlineColor:UInt;
	public var lineThickness:Float;
	public var mouseOver:Signal<MouseEvent>;
	public var mouseOut:Signal<MouseEvent>;
	public var mouseClick:Signal<MouseEvent>;

	public function new(){}
	
	public function initialize(parent:DisplayObjectContainer,poly:Array<Vector2>):Collider
	{
		fillColor = styleConfig.defaultColliderFillColor;
		outlineColor = styleConfig.defaultColliderLineColor;
		lineThickness = styleConfig.defaultColliderLineThickness;
		visible = false;
		collisionPoly = poly;
		collisionFrame = new Frame2(new Vector2(0,0),0);

		collisionShape = new Sprite();

		GraphicsExt.drawPoly(collisionShape.graphics,poly,lineThickness,fillColor,outlineColor);
		
		mouseOver =   SignalUtil.makeSignal(collisionShape,MouseEvent.MOUSE_OVER);
		mouseOut  =   SignalUtil.makeSignal(collisionShape,MouseEvent.MOUSE_OUT);
		mouseClick =  SignalUtil.makeSignal(collisionShape,MouseEvent.CLICK);
		parent.addChild(collisionShape);
		return this;

	}
	
}