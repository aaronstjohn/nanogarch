package nanogarch.components;
import nanogarch.model.TerrainType;
class Terrain
{
	public var terrainType:TerrainType;
	public function new(){}
	public function initialize(type:TerrainType):Terrain
	{
		terrainType = type;
		return this;
	}
}