using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class PlanetaryGridCellAlignmentSystem : ReactiveSystem
{
	readonly Context _context;
    RaycastHit hit;
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
            var gridCellEnt = _context.GetEntityWithGridCellId(e.inGridCell.id);

            //CURRENTLY ALIGNS WITH ARBITRARY NEIGHBOR  NEED TO SETUP HEADING SYSTEM TO BE BETTER THAN THIS
            var neighborEnt = _context.GetEntityWithGridCellId(gridCellEnt.gridCell.neighbors[0]);

            Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
  
            GameObject unitGo = e.view.gameObject;
    		
            //JUST HERE FOR TESTING PURPOSES!!
            // unitGo.transform.position = cellCentroid;
    		
            Vector3 b = Vector3.ProjectOnPlane(neighborEnt.gridCell.centroid, cellCentroid.normalized);
    		Quaternion rotation = Quaternion.LookRotation(b,cellCentroid.normalized);
            unitGo.transform.rotation = rotation;
    	
           
    	}
  
    }
}