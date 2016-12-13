package nanogarch;
import ash.core.Entity;
import ash.core.Engine;
import nanogarch.components.Unit;
import nanogarch.components.Position;
import nanogarch.components.Display;

import nanogarch.graphics.UnitView;


class EntityCreator
{
    @inject public var engine:Engine;

    public function new(){}

    public function destroyEntity(entity:Entity):Void
    {
        engine.removeEntity(entity);
    }
    public function createUnit()
    {
    	var unit : Entity = new Entity();
        
        unit.add( new Unit(  ) )
        	.add( new Position( 0, 0, 0 ) )
            .add( new Display( new UnitView() ) );
		engine.addEntity(unit);

        return unit;
    }
}