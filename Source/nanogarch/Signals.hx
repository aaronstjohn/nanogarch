package nanogarch;
import minject.Injector; 
import ash.core.Entity;
using tink.CoreApi;

// import nanogarch.map.Hex;
// typedef HexSignal = Signal<Hex>;
// typedef HexSignalTrigger = SignalTrigger<Hex>;

// collider.mouseOver.handle(function(e:MouseEvent){
//             cellInjector.map(MouseEvent).toValue(e);
//             var sig=injector.getInstance(Signals.TerrainInfoSignalPair);
//             sig.trigger.trigger(cellInjector);
//             cellInjector.unmap(MouseEvent);
//         });

class SignalPair<T>
{
	public var trigger(get,null):SignalTrigger<T>;
	public var signal(get,null):Signal<T>;
	
	public function new(){
		this.signal=this.trigger=Signal<T>.trigger();
	}
}
class InjectorSignal extends SignalPair<Injector>
{

}
class TerrainInfo extends SignalPair<Injector> {public function new(){}}
class MovementQuery:SignalPair<Injector>{public function new(){}}

// typedef TerrainInfoCommandSignal = Signal<Entity>;
// typedef TerrainInfoSignal = Signal<Noise>;