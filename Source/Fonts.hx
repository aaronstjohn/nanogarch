package;
 
import openfl.text.Font;
import openfl.Assets;
 
class Fonts {
    public static var M_PLUS_1M_REGULAR(default, null):String;
    public static var M_PLUS_1M_BOLD(default, null):String;
 
    public static function init():Void {
    #if js
        M_PLUS_1M_REGULAR = Assets.getFont("assets/fonts/mplus-1m-regular.ttf").fontName;
        M_PLUS_1M_BOLD = Assets.getFont("assets/fonts/mplus-1m-bold.ttf").fontName;
    #else
        Font.registerFont(Mplus1mRegular);
        M_PLUS_1M_REGULAR = (new Mplus1mRegular()).fontName;
        Font.registerFont(Mplus1mBold);
        M_PLUS_1M_BOLD = (new Mplus1mBold()).fontName;
    #end
    }
}
 
@:font("assets/fonts/mplus-1m-regular.ttf")
private class Mplus1mRegular extends Font {}
@:font("assets/fonts/mplus-1m-bold.ttf")
private class Mplus1mBold extends Font {}