using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class AddCommandSystem : ReactiveSystem
{
	readonly Context _context;
    public AddCommandSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Commands,GroupEvent.Added);
        
    }

    protected override bool Filter(Entity entity) {
        return entity.isUnit;
    }
    protected override void Execute(List<Entity> entities) {

        foreach(var e in entities)
        {
            foreach(var c in e.commands)
            {
                Debug.Log("FOUND COMMAND "+c.GetType());
            }
        }
    	// var planetEntity = _context.planetaryGridEntity;
     //    GameObject planetGo = planetEntity.view.gameObject;
    	// foreach(var e in entities)
    	// {
    	// 	var gridCellEnt = _context.GetEntityWithGridCellId(e.spawn.gridCellId);
     //        Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
     //        GameObject unitGo = e.view.gameObject;
    	// 	unitGo.transform.position = cellCentroid;
    		

    	// 	float angle = Vector3.Angle( Vector3.up,cellCentroid.normalized );
    	// 	unitGo.transform.Rotate(new Vector3(-angle,0,0));
    	// 	unitGo.transform.parent = planetGo.transform;
     //        e.AddInGridCell(gridCellEnt.gridCell.id);
    	// 	e.RemoveSpawn();
    	// }
  
    }
	// public void Initialize() 
	// {
		
	// }

}