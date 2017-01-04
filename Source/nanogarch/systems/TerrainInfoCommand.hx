
package nanogarch.systems;
import ash.core.Entity;
import nanogarch.components.Terrain; 
using tink.CoreApi; 
class TerrainInfoCommand extends Command
{
	@inject public var terrain:Terrain;

	public function new(){
		super();
	}
	override public function Execute():Void 
	{
		// var t = entity.get(Terrain);
		trace("YAY: "+terrain.terrainType);
	}
}