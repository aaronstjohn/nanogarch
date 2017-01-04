package nanogarch;
import ash.core.Entity;
import ash.core.Engine;
import nanogarch.graphics.*;
// import nanogarch.components.Unit;
// import nanogarch.components.MapPosition;
// import nanogarch.components.HexFrame;
// import nanogarch.components.Frame;
// import nanogarch.components.Display;
// import nanogarch.components.Collider;
import nanogarch.components.*;

// import nanogarch.graphics.UnitView;
// import nanogarch.graphics.MapView;
// import nanogarch.map.HexMap;
// import nanogarch.components.MapCell;
import nanogarch.systems.*;
import nanogarch.geom.Polygons;
import minject.Injector;
import openfl.events.MouseEvent;

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
    // public function bindCommand<A:Command>(entity:Entity)
    // {
    //    var commandInjector = injector.createChildInjector();
    //    commandInjector.map(Entity).toValue(entity);
    //    var command =  commandInjector.getInstance(A);
    //    command.Execute();
    // }
    public function createUnit(unitType:String)
    {
        var unit:Entity = injector.instantiate(Entity);
        var view:UnitView = injector.getInstance(UnitView).initialize();
        var display:Display = injector.getInstance(Display).initialize(view);
        var unitComp:Unit = injector.getInstance(Unit).initialize(unitType);
        unit.add(injector.getInstance(Frame));
        unit.add(injector.getInstance(HexFrame));
        unit.add(display);
        unit.add(unitComp);

        // var unitInjector = injector.createChildInjector();
        // unitInjector.map(Entity).toValue(unit);
        // unitInjector.map(Unit).toValue(unitComp);
        // unit.add(unitInjector);

        return unit;

    }
    public function createWordCell(terrainType:String)
    {
        var cell:Entity = injector.getInstance(Entity);
        var hexPoly = Polygons.hex(config.worldHexOrientation,config.worldHexScale);
        var view:WorldCellView = injector.getInstance(WorldCellView).initialize(hexPoly);
        var display:Display = injector.getInstance(Display).initialize(view);
        var collider:Collider = injector.getInstance(Collider).initialize(display.displayObject,hexPoly);
        var terrain:Terrain = injector.getInstance(Terrain).initialize(terrainType);
        cell.add(injector.getInstance(Frame));
        cell.add(injector.getInstance(HexFrame));
        cell.add(collider);
        cell.add(display);
        cell.add(terrain);

        // var cellInjector = injector.createChildInjector();
        // cellInjector.map(Entity).toValue(cell);
        // cellInjector.map(Terrain).toValue(terrain);
        // cell.add(cellInjector);
        
       
        
        // ///RELAYS THE MOUSE EVENT TO THE TERRAIN INFO SIGNAL 
        // collider.mouseOver.handle(function(e:MouseEvent){
        //     cellInjector.map(MouseEvent).toValue(e);
        //     var sig=injector.getInstance(Signals.TerrainInfoSignalPair);
        //     sig.trigger.trigger(cellInjector);
        //     cellInjector.unmap(MouseEvent);
        // });
        
       

        return cell;
    }
    public function createCellInfo()
    {
        var cell:Entity = injector.getInstance(Entity);
        var view:CellInfoView = injector.getInstance(CellInfoView).initialize();

        cell.add(injector.getInstance(Frame));
        cell.add(injector.getInstance(Display).initialize(view));
    }
}
// class SignalRouter<T>
// {
//     public static function route<T>(signal:Signal<T>,injector:Injector,type:Type)
//     {

//     }
// }