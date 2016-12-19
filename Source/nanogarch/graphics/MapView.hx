package nanogarch.graphics;

import openfl.display.Sprite;
import nanogarch.map.HexMap;
import nanogarch.map.ScreenCoordinate;
import openfl.text.TextField;
import openfl.text.TextFieldType;
import openfl.text.TextFormat;
import openfl.text.TextFieldAutoSize;
import openfl.text.TextFormatAlign;
class MapView extends Sprite
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
            
            var spr: Sprite = new Sprite();
            // spr.graphics.beginFill(0xffffff);
            // spr.graphics.lineStyle (1, 0x000000);
            // spr.graphics.drawCircle(0,0,10);
            
            addChild(spr);
            spr.x= coord.x;
            spr.y=coord.y;
            var format = new TextFormat(Fonts.M_PLUS_1M_BOLD, 10);

            // var size = 10;
            // var format: TextFormat = new TextFormat();
            // // format.font = "fonts/Sen-Regular.ttf";
            // format.size = Std.int(size);
            // format.color = 0x000000;
            // // format.align = TextFormatAlign.CENTER;

            var text = new TextField();
            text.selectable = false;
            text.defaultTextFormat = format;
           // text.autoSize=TextFieldAutoSize.CENTER;
            text.type = TextFieldType.DYNAMIC;
            text.text = hex.toString();
            text.x = -(text.textWidth+4)/2;
            text.y = -(text.textHeight+4)/2;
            spr.addChild(text);
           //  // text.width = text.textWidth + 4;
           //  // text.height = text.textHeight + 4;
            
           //  text.x = coord.x;//-text.textWidth;//- (text.textWidth)/2; //-text.width/2;
           //  text.y = coord.y;//-(text.textHeight)/2;// - text.height/2;
           //  addChild(text);
           //  // text.border = false;
           //  // text.borderColor = 0x000000;

            


        }
        
    }

}