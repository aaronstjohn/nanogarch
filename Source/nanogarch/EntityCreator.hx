package nanogarch;
import ash.core.Entity;
import ash.core.Engine;
import nanogarch.components.Unit;
import nanogarch.components.MapPosition;
import nanogarch.components.Frame;
import nanogarch.components.Display;

import nanogarch.graphics.UnitView;
import nanogarch.graphics.MapView;
import nanogarch.map.HexMap;
import minject.Injector;


class EntityCreator
{
    @inject public var engine:Engine;
    @inject("MainMap") public var hexmap:HexMap;
    @inject public var injector:Injector;
    public function new(){}

    public function destroyEntity(entity:Entity):Void
    {
        engine.removeEntity(entity);
    }
    // public function createTurn()
    // {
    //     var turn:Entity = new Entity();
    //     turn.add(new GameState())
    //         .add(new GameMap())
    //         .add(new TurnActions())
    //     // engine.addEntity(turn);
    // }
    public function createMap()
    {
        var map: Entity = new Entity();
        var mapview = new MapView();
        injector.injectInto(mapview);
        map.add(new Frame())
           .add(new Display(mapview));
        // engine.addEntity(map);
        return map;
    }
    public function createUnit()
    {
    	var unit : Entity = new Entity();
        
        unit.add( new Frame() )
            .add(new MapPosition())
        	.add( new Display( new UnitView() ) );
		// engine.addEntity(unit);

        return unit;
    }
}