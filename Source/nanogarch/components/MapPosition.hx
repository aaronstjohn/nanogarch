package nanogarch.components;
import nanogarch.map.HexDirection;
import nanogarch.map.Hex;

using tink.CoreApi;
class MapPosition
{
	public var moveRequest(default, null):Signal<HexDirection>;
	var moveRequestTrigger:SignalTrigger<HexDirection>;

	public var positionChanged(default, null):Signal<Hex>;
	var positionChangedTrigger:SignalTrigger<Hex>;
	public var position:Hex;
	public function new (x:Int = 0, y:Int = 0,z:Int=0)
	{
		position = new Hex(x,y,z);
		moveRequest= moveRequestTrigger = Signal.trigger();
		positionChanged= positionChangedTrigger = Signal.trigger();

	}
	public function moveTo(hex:Hex)
	{
		position=hex;
		positionChangedTrigger.trigger(hex);
		// moveRequestTrigger.trigger(hex);
	}
	public function requestMoveInDirection(direction:HexDirection)
	{
		moveRequestTrigger.trigger(direction);
	}
	// public var moveRequest:Signal1<Direction>;
    // public var changed(default, null):Signal2<Int, Int>;
}
