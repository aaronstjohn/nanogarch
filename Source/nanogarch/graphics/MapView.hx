package nanogarch.graphics;

import openfl.display.Shape;
import nanogarch.map.HexMap;
import nanogarch.map.ScreenCoordinate;

class MapView extends Shape
{
    public function new(map:HexMap)
    {
        super();
        var polyPoints =map.grid.polygonVertices();
        for( hex in map.grid.hexes)
        {
        	var coord:ScreenCoordinate = map.grid.hexToCenter(hex);
        	graphics.beginFill(0xA2C0F2,0.5);
        	graphics.lineStyle (1, 0x000000);
        	graphics.moveTo(polyPoints[0].x+coord.x,polyPoints[0].y+coord.y);
			graphics.lineTo(polyPoints[1].x+coord.x,polyPoints[1].y+coord.y);
			graphics.lineTo(polyPoints[2].x+coord.x,polyPoints[2].y+coord.y);
			graphics.lineTo(polyPoints[3].x+coord.x,polyPoints[3].y+coord.y);
			graphics.lineTo(polyPoints[4].x+coord.x,polyPoints[4].y+coord.y);
			graphics.lineTo(polyPoints[5].x+coord.x,polyPoints[5].y+coord.y);
			graphics.lineTo(polyPoints[0].x+coord.x,polyPoints[0].y+coord.y);

			graphics.endFill();


        }
        
    }

}