package nanogarch.graphics;
import openfl.display.Sprite;
import util.GraphicsExt;
import hxmath.math.Vector2;
import nanogarch.StyleConfig;
import openfl.text.TextField;
import openfl.text.TextFieldType;

class CellInfoView extends Sprite
{
	@inject public var styleConfig:StyleConfig;

	var title:TextField;

	public function new(){
		super();
	}
	public function initialize():CellInfoView
	{
		//var format = new TextFormat(Fonts.M_PLUS_1M_BOLD, 10);
        title = new TextField();
        title.selectable = false;

        title.defaultTextFormat = styleConfig.defaultTextFormat;
        title.type = TextFieldType.DYNAMIC;
        title.text ="TITLE:";

        //text.x = -(text.textWidth+4)/2;
        //text.y = -(text.textHeight+4)/2;
        title.mouseEnabled = false;
        addChild(title);
        return this;
       // hexView.addChild(text);
	}	
}