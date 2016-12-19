package nanogarch.graphics;

import openfl.display.Sprite;
import nanogarch.map.HexMap;
import nanogarch.map.Hex;
import nanogarch.map.ScreenCoordinate;
import openfl.text.TextField;
import openfl.text.TextFieldType;
import openfl.text.TextFormat;
import openfl.text.TextFieldAutoSize;
import openfl.text.TextFormatAlign;

import openfl.events.MouseEvent;
class MapView extends Sprite
{
    public function new(map:HexMap)
    {
        super();
        var polyPoints =map.grid.polygonVertices();
        for( hex in map.grid.hexes)
        {
        	var coord:ScreenCoordinate = map.grid.hexToCenter(hex);
        	
            
            // var hexView: HexView = new HexView(hex);
            var hexView = new HexView(hex);
            // hexView.hex = hex;
            hexView.graphics.beginFill(0xA2C0F2);
            hexView.graphics.lineStyle (1, 0x000000);
            hexView.graphics.moveTo(polyPoints[0].x,polyPoints[0].y);
            hexView.graphics.lineTo(polyPoints[1].x,polyPoints[1].y);
            hexView.graphics.lineTo(polyPoints[2].x,polyPoints[2].y);
            hexView.graphics.lineTo(polyPoints[3].x,polyPoints[3].y);
            hexView.graphics.lineTo(polyPoints[4].x,polyPoints[4].y);
            hexView.graphics.lineTo(polyPoints[5].x,polyPoints[5].y);
            hexView.graphics.lineTo(polyPoints[0].x,polyPoints[0].y);

            hexView.graphics.endFill();
            
            addChild(hexView);
            hexView.x= coord.x;
            hexView.y=coord.y;
            var format = new TextFormat(Fonts.M_PLUS_1M_BOLD, 10);
            var text = new TextField();
            text.selectable = false;
            text.defaultTextFormat = format;
            text.type = TextFieldType.DYNAMIC;
            text.text = hex.toString();
            text.x = -(text.textWidth+4)/2;
            text.y = -(text.textHeight+4)/2;
            text.mouseEnabled = false;
            hexView.addChild(text);
            hexView.addEventListener (MouseEvent.MOUSE_OVER, onMouseOverHex);
            hexView.addEventListener (MouseEvent.MOUSE_OUT, onMouseOutHex);

        }

        
    }
    public function onMouseOverHex(e:MouseEvent):Void
    {
        // trace("Mouse OVER hex ! " + cast(e.currentTarget,HexView).hex );
        cast(e.currentTarget,HexView).alpha=0.5;
    }  
    public function onMouseOutHex(e:MouseEvent):Void
    {
        // trace("Mouse OUT hex ! " + cast(e.currentTarget,HexView).hex );
        cast(e.currentTarget,HexView).alpha=1;
    }    

}
class HexView extends Sprite
{
    public var hex:Hex;
    public function new (hex:Hex){
        super();
        this.hex = hex;
    }
}
