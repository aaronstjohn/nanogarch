using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CreatePlanetaryGridSystem : ReactiveSystem, IInitializeSystem {
	readonly Context _context;
    // readonly Group _planetGridElements;
	public CreatePlanetaryGridSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
		// _planetGridCells = _context.GetGroup(Matcher.AllOf())

	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.View);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasPlanetaryGrid;
    }
    protected override void Execute(List<Entity> entities) {
    	var entity = entities.SingleEntity();
    	var planet = entity.view.gameObject;
    	MeshFilter filter = planet.GetComponent<MeshFilter>();
    	Mesh mesh = entity.planetaryGrid.geometry.CreateMesh("PlanetaryGridMesh");
    	filter.sharedMesh = mesh;
		mesh.RecalculateBounds();
    	Debug.Log("View Added to planet grid"+entity.view.gameObject);
    }
	public void Initialize() {
		TruncatedIcosahedron geom = new TruncatedIcosahedron(0.5f, 4);
		List<RadialPolyTris> polys = geom.GetRadialPolys();
		for(int i=0;i<polys.Count;i++)
		{
			_context.CreateEntity()
				.AddPlanetaryGridPolygon(i,polys[i]);
		}

		_context.CreateEntity()
            .AddPlanetaryGrid(geom)
            .IsInteractive(true)
            .AddName("PlanetaryGrid")
			.AddResource("PlanetaryGrid");

	}
}