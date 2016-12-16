package nanogarch;
import haxe.Timer;
import util.Gaussian;
using tink.CoreApi;

class NanogarchContract
{
	// public function submitOrder(order:Order):Void{}
	public var turnResolved(default, null):Signal<Noise>;
	var turnTrigger:SignalTrigger<Noise>;
	public function new() {
     
        turnResolved= turnTrigger = Signal.trigger();
        turnResolved.handle(handleTurnResolved);
        startTurn();
    }
    public function startTurn():Void
    {
    	var seconds = Gaussian.getGaussian(14,3);
    	var milliseconds = Std.int(seconds*1000);
    	trace("Triggering turn in : "+seconds+" seconds ");
    	Timer.delay(function () turnTrigger.trigger(Noise),milliseconds);
    }

    function handleTurnResolved():Void
    {
    	startTurn();
    }
}