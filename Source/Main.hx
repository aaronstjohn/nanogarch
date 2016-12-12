package;
// package nanogarch;
import nanogarch.Nanogarch;
import hex.*;
import minject.Injector;
import Type;

import openfl.display.Sprite;
import openfl.display.DisplayObjectContainer;

class Main extends Sprite {
	
	
	public function new () {
		
		super ();
		var injector:Injector = Nanogarch.configure();
	  	var game:Nanogarch = injector.getInstance(Nanogarch);
	  	injector.map(DisplayObjectContainer,"GameDisplayObject").toValue(this);
	  	game.initialize();
		trace("Hello");
		
	}
	
	
}