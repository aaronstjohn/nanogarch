using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class SpawnSystem : ReactiveSystem {

	readonly Context _context;
    public SpawnSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector( Matcher.AllOf(CoreMatcher.Spawn,CoreMatcher.View));
    }

    protected override bool Filter(Entity entity) {
        return entity.hasView ;
    }
    protected override void Execute(List<Entity> entities) {
    	var planetEntity = _context.planetaryGridEntity;
        GameObject planetGo = planetEntity.view.gameObject;
    	foreach(var e in entities)
    	{
           
    		var gridCellEnt = _context.GetEntityWithGridCellId(e.spawn.gridCellId);
            Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
            GameObject unitGo = e.view.gameObject;
    		
    		unitGo.transform.parent = planetGo.transform;
            e.AddInGridCell(gridCellEnt.gridCell.id);
    		e.RemoveSpawn();
    	}
  
    }
	// public void Initialize() {
		
	// 	_context.CreateEntity()
 //            .IsUnit(true)
 //            .AddMovement(1)
 //            .IsFortifiable(true)
 //            .AddSpawn(36)
 //            .AddName("Unit")
	// 		.AddResource("Tank");

	// }
}