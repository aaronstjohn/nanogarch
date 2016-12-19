package;
// package nanogarch;
import nanogarch.Nanogarch;

import minject.Injector;
import Type;
import flash.events.Event;

import openfl.display.Sprite;
import openfl.display.DisplayObjectContainer;

class Main extends Sprite {
	
	
	public function new () {
		
		super ();
		Fonts.init();
		
		addEventListener(Event.ENTER_FRAME, onEnterFrame);
    }

    private function onEnterFrame(event:Event):Void
    {
        removeEventListener(Event.ENTER_FRAME, onEnterFrame);
        var injector:Injector = Nanogarch.configure(this,stage.stageWidth, stage.stageHeight );
	  	var game:Nanogarch = injector.getInstance(Nanogarch);
	  	game.initialize();
	  	game.start();
        // var asteroids = new Asteroids( this, stage.stageWidth, stage.stageHeight );
        // asteroids.start();
    }
	
	
}