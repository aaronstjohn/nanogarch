package util;
using tink.CoreApi;
import openfl.events.Event;
import openfl.events.IEventDispatcher; 

class SignalUtil
{
	public static function makeSignal<A:Event>(dispatcher:IEventDispatcher, type:String):Signal<A> 
	{
	    return Signal.ofClassical(
	        dispatcher.addEventListener.bind(type),
	        dispatcher.removeEventListener.bind(type)
	    );
	}
	
}