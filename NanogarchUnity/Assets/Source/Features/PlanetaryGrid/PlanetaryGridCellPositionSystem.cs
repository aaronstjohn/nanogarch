using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class PlanetaryGridCellPositionSystem : ReactiveSystem
{
	readonly Context _context;
    RaycastHit hit;
    public PlanetaryGridCellPositionSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
         return context.CreateCollector(CoreMatcher.InGridCell,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasView;
    }
    protected override void Execute(List<Entity> entities) {
    	foreach(var e in entities)
    	{
            var gridCellEnt = _context.GetEntityWithGridCellId(e.inGridCell.id);
            Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
            GameObject unitGo = e.view.gameObject;
            unitGo.transform.position = cellCentroid;
           
    	}
  
    }
}