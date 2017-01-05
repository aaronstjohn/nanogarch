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
	public var signalTrigger(default,null):SignalTrigger<T>;
	public var signal(default,null):Signal<T>;
	
	public function new(){
		this.signal=this.signalTrigger=Signal.trigger();
	}
	public function trigger(t:T){signalTrigger.trigger(t);}
}
class StartSignal extends SignalPair<Noise>{public function new(){super();}}

class OverMapCellSignal extends SignalPair<Entity>{public function new(){super();}}
class OutMapCellSignal extends SignalPair<Entity>{public function new(){super();}}
class ClickMapCellSignal extends SignalPair<Entity>{public function new(){super();}}

// class InjectorSignal extends SignalPair<Injector>
// {

// }
// class TerrainInfo extends SignalPair<Injector> {public function new(){}}
// class MovementQuery:SignalPair<Injector>{public function new(){}}

// typedef TerrainInfoCommandSignal = Signal<Entity>;
// typedef TerrainInfoSignal = Signal<Noise>;