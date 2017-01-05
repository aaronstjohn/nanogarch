package;
import nanogarch.ConfigurationFactory;
import minject.Injector;
import Type;
using tink.CoreApi;
import nanogarch.Signals;
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
			    				.generateRandomMap()
                                .configureSignals().injector;
			
        injector.getInstance(nanogarch.Signals.StartSignal).trigger(Noise);    				 
    	// injector.getInstance(Nanogarch).start();
    }
}