package nanogarch;
import ash.core.Entity;
import ash.core.Engine;
import nanogarch.graphics.WorldCellView;
// import nanogarch.components.Unit;
// import nanogarch.components.MapPosition;
import nanogarch.components.HexFrame;
import nanogarch.components.Frame;
import nanogarch.components.Display;
import nanogarch.components.Collider;
import nanogarch.components.Terrain;

// import nanogarch.graphics.UnitView;
// import nanogarch.graphics.MapView;
// import nanogarch.map.HexMap;
// import nanogarch.components.MapCell;
import nanogarch.geom.Polygons;
import minject.Injector;


class EntityCreator
{
    @inject public var engine:Engine;
    @inject public var config:GameConfig;
    @inject public var injector:Injector;
    public function new(){}

    public function destroyEntity(entity:Entity):Void
    {
        engine.removeEntity(entity);
    }
    public function createWordCell()
    {
        var cell:Entity = injector.getInstance(Entity);
        var hexPoly = Polygons.hex(config.worldHexOrientation,config.worldHexScale);
        var view:WorldCellView = injector.getInstance(WorldCellView).initialize(hexPoly);

        cell.add(injector.getInstance(Frame));
        cell.add(injector.getInstance(HexFrame));
        cell.add(injector.getInstance(Collider).initialize(hexPoly));
        cell.add(injector.getInstance(Display).initialize(view));
        cell.add(injector.getInstance(Terrain).initialize("grass"));

        return cell;
    }
    public function createCellInfo()
    {
        var cell:Entity = injector.getInstance(Entity);
        cell.add(injector.getInstance(Frame));
        // cell.add(injector.getInstance(Display).initialize(view));
    }
    // public function createTurn()
    // {
    //     var turn:Entity = new Entity();
    //     turn.add(new GameState())
    //         .add(new GameMap())
    //         .add(new TurnActions())
    //     // engine.addEntity(turn);
    // }
    
    // public function createMap()
    // {
    //     var map: Entity = new Entity();
    //     var mapview = new MapView();
    //     injector.injectInto(mapview);
    //     map.add(new Frame())
    //        .add(new Display(mapview));
    //     // engine.addEntity(map);
    //     return map;
    // }
  //   public function createUnit()
  //   {
  //   	var unit : Entity = new Entity();
        
  //       unit.add( new Frame() )
  //           .add(new MapPosition())
  //       	.add( new Display( new UnitView() ) );
		// // engine.addEntity(unit);

  //       return unit;
  //   }
}