package;
import nanogarch.Nanogarch;
import nanogarch.ConfigurationFactory;
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
    	var configFactory = new ConfigurationFactory(this,stage.stageWidth, stage.stageHeight );
    	var injector:Injector = configFactory
                                .configureSystems()
                                .configureEntities()
			    				.configureMap()
			    				.generateRandomMap().injector;
			    				 
    	injector.getInstance(Nanogarch).start();
    }
}