using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class SpawnUnitSystem : ReactiveSystem, IInitializeSystem {

	readonly Context _context;
    public SpawnUnitSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(Matcher.AllOf(CoreMatcher.View,CoreMatcher.Spawn));
    }

    protected override bool Filter(Entity entity) {
        return entity.isUnit ;
    }
    protected override void Execute(List<Entity> entities) {
    	var planetEntity = _context.planetaryGridEntity;
        GameObject planetGo = planetEntity.view.gameObject;
    	foreach(var e in entities)
    	{
    		var gridCellEnt = _context.GetEntityWithGridCellId(e.spawn.gridCellId);
            Vector3 cellCentroid =  gridCellEnt.gridCell.centroid;
            GameObject unitGo = e.view.gameObject;
    		unitGo.transform.position = cellCentroid;
    		

    		float angle = Vector3.Angle( Vector3.up,cellCentroid.normalized );
    		unitGo.transform.Rotate(new Vector3(-angle,0,0));
    		unitGo.transform.parent = planetGo.transform;
            e.AddInGridCell(gridCellEnt.gridCell.id);
    		e.RemoveSpawn();
    	}
  
    }
	public void Initialize() {
		
		_context.CreateEntity()
            .IsUnit(true)
            .AddSpawn(36)
            .AddName("Unit")
			.AddResource("Tank");

	}
}