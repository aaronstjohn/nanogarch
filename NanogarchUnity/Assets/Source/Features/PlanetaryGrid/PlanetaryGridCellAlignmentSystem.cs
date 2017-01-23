using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class PlanetaryGridCellAlignmentSystem : ReactiveSystem
{
	readonly Context _context;
    public PlanetaryGridCellAlignmentSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.InGridCell,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasView;//entity.isUnit ;
    }
    protected override void Execute(List<Entity> entities) {
    	var planetEntity = _context.planetaryGridEntity;
        GameObject planetGo = planetEntity.view.gameObject;
    	foreach(var e in entities)
    	{
            Debug.Log("ALIGNING UNIT");
    		var gridCellEnt = _context.GetEntityWithGridCellId(e.inGridCell.id);
            Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
            GameObject unitGo = e.view.gameObject;
    		unitGo.transform.position = cellCentroid;
    		

    		float angle = Vector3.Angle( Vector3.up,cellCentroid.normalized );
    		unitGo.transform.Rotate(new Vector3(-angle,0,0));
    		// unitGo.transform.parent = planetGo.transform;
           
    	}
  
    }
}