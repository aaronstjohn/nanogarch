package nanogarch.creators;
import ash.core.Entity;
import minject.Injector;
import nanogarch.components.Collider;

import nanogarch.graphics.ShapeView;
import nanogarch.model.TerrainType;
import nanogarch.components.Terrain;
import nanogarch.components.Frame;
import nanogarch.components.MapGridFrame;
import nanogarch.components.Display;

import Random;

class RandomTileCreator implements ICreator<Entity>
{
	//Creates a rando - terrain tile 
	@inject public var injector:Injector;
	@inject public var styleConfig:StyleConfig;
	@inject public var gameConfig:GameConfig;
	public function new(){}
	public function create():Entity
	{
		var tile:Entity = injector.getInstance(Entity);
                // var hexPoly = Polygons.hex(config.worldHexOrientation,config.worldHexScale);
                // var view:WorldCellView = injector.getInstance(WorldCellView).initialize(hexPoly);
                // var display:Display = injector.getInstance(Display).initialize(view);
                // var collider:Collider = injector.getInstance(Collider).initialize(display.displayObject,hexPoly);
                var terrain:Terrain = injector.getInstance(Terrain).initialize(Random.enumConstructor(TerrainType));

                var style = styleConfig.terrainStyles.get(terrain.terrainType);
                var view:ShapeView = injector.getInstance(ShapeView).initialize(gameConfig.referenceHexPolygon,style);
                var display:Display = injector.getInstance(Display).initialize(view);
                var collider:Collider = injector.getInstance(Collider).initialize(display.displayObject,gameConfig.referenceHexPolygon);
                tile.add(injector.getInstance(Frame));
                tile.add(injector.getInstance(MapGridFrame));
                // // cell.add(collider);
                tile.add(display);
                tile.add(collider);
                tile.add(terrain);
                // tile.add(view);
                return tile;
	}
	public function toString():String
	{
		return "RandomTileCreator";
	}
}