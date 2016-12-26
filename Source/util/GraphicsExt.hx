package util;
import openfl.display.Graphics;

import hxmath.math.Vector2;
class GraphicsExt
{
	public static function drawPoly(graphics:Graphics,points:Array<Vector2>,lineThickness:Float,fillColor:UInt,lineColor:UInt)
	{
		graphics.beginFill(fillColor);
                graphics.lineStyle (lineThickness, lineColor);

                graphics.moveTo(points[0].x,points[0].y);
                for (i in 1...points.length)
                	graphics.lineTo(points[i].x,points[i].y);
                graphics.lineTo(points[0].x,points[0].y);
                graphics.endFill();
	}
}