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
		var injector:Injector = Nanogarch.configure(this);
	  	var game:Nanogarch = injector.getInstance(Nanogarch);
	  	
	  	game.initialize();
		
	}
	
	
}