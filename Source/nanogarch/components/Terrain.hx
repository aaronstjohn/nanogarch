package nanogarch.components;
class Terrain
{
	public var terrainType:String;
	public function new(){}
	public function initialize(type:String):Terrain
	{
		terrainType = type;
		return this;
	}
}