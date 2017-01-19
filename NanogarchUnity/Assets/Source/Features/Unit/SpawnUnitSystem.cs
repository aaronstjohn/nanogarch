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
    	foreach(var e in entities)
    	{
    		var gridEntity = _context.GetEntityWithPlanetaryGridPolygonId(e.spawn.spawnGridPolyId);
    		e.view.gameObject.transform.position = gridEntity.planetaryGridPolygon.centroid;
    		

    		float angle = Vector3.Angle( Vector3.up,gridEntity.planetaryGridPolygon.centroid.normalized );
    		e.view.gameObject.transform.Rotate(new Vector3(-angle,0,0));
    		e.view.gameObject.transform.parent = planetEntity.view.gameObject.transform;
    		// e.LookAt(planetEntity);
    		e.RemoveSpawn();
    	}
  //   	var entity = entities.SingleEntity();
  //   	var planet = entity.view.gameObject;
  //   	MeshFilter filter = planet.GetComponent<MeshFilter>();
  //   	Mesh mesh = entity.planetaryGrid.geometry.CreateMesh("PlanetaryGridMesh");
  //   	filter.sharedMesh = mesh;
		// mesh.RecalculateBounds();
  //   	Debug.Log("View Added to planet grid"+entity.view.gameObject);
    }
	public void Initialize() {
		
		_context.CreateEntity()
            .IsUnit(true)
            .AddSpawn(36)
            .AddName("Unit")
			.AddResource("Tank");

	}
}