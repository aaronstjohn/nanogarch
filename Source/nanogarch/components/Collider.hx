package nanogarch.components;
import hxmath.frames.Frame2;
import hxmath.math.Vector2;
import openfl.display.Shape;
import openfl.display.Sprite;
import util.GraphicsExt;
import openfl.events.Event;
import openfl.events.MouseEvent;
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

	public function new(){}
	// function test(ev:MouseEvent)
	// {
	// 	trace("TESSSTTTT");
	// }
	public function initialize(poly:Array<Vector2>):Collider
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
		
		// mouseOverCollider = SignalUtil.makeSignal<MouseEvent>(collisionShape,MouseEvent.MOUSE_OVER);
		// SignalUtil.makeSignal(node.collider.collisionShape,MouseEvent.MOUSE_OVER);
		// SignalUtil.makeSignal(node.collider.collisionShape,MouseEvent.MOUSE_OVER);
		return this;

	}
	// public function registerSignals()
	// {
	// 	mouseOver =   SignalUtil.makeSignal(collisionShape,MouseEvent.MOUSE_OVER);
	// 	mouseOver.handle(test);
	// 	collisionShape.addEventListener(MouseEvent.MOUSE_OVER,test);
	// 	trace ("MOUSE OVER "+mouseOver);
	// }
}