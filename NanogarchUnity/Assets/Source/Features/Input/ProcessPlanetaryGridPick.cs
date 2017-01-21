using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;
public sealed class ProcessPlanetaryGridPickSystem : ReactiveSystem, ICleanupSystem
{
	readonly Contexts _contexts;
	readonly Group _gridPicks;
    public ProcessPlanetaryGridPickSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
        _gridPicks  = contexts.core.GetGroup(CoreMatcher.Picked);
    
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(InputMatcher.Pick,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.pick.gameObject.name=="PlanetaryGrid";
    }

    protected override void Execute(List<Entity> entities) {
        var pickEntity = entities.SingleEntity();
        var pick = pickEntity.pick;
        var planetEntity = _contexts.core.planetaryGridEntity;
        int pickedGridCellId =  planetEntity.planetaryGrid.geometry.NearestPoly(pick.point);

        //Get the picked Grid cell
        var gridCellEntity = _contexts.core.GetEntityWithGridCellId(pickedGridCellId);
        gridCellEntity.isPicked = true;
        Debug.Log("Picked grid cell "+pickedGridCellId);
        var cellContents = _contexts.core.GetEntitiesInGridCell(pickedGridCellId);
        foreach(var e in cellContents)
        {
        	Debug.Log("Picked cell based entity "+e.name.id);
        	e.isPicked =true;
        }
      
    }
    public void Cleanup() {
        foreach(var p in _gridPicks.GetEntities())
        	p.isPicked = false ;
    }
}