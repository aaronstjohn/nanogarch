package nanogarch;
import ash.core.Entity;
import ash.core.Engine;
import nanogarch.components.Unit;
import nanogarch.components.GridPosition;
import nanogarch.components.Frame;
import nanogarch.components.Display;

import nanogarch.graphics.UnitView;
import nanogarch.graphics.MapView;
import hex.Grid;


class EntityCreator
{
    @inject public var engine:Engine;
    @inject("MainGrid") public var grid:Grid;
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
        map.add(new Frame())
           .add(new Display(new MapView(grid)));
        engine.addEntity(map);
    }
    public function createUnit()
    {
    	var unit : Entity = new Entity();
        
        unit.add( new Frame() )
            .add(new MapPosition())
        	.add( new Display( new UnitView() ) );
		engine.addEntity(unit);

        return unit;
    }
}