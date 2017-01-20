using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class InitializePlanetaryGridSystem : ReactiveSystem, IInitializeSystem {
	readonly Context _context;
    public InitializePlanetaryGridSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
	 protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.View,GroupEvent.Added);
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
    }
	public void Initialize() {
		TruncatedIcosahedron geom = new TruncatedIcosahedron(0.5f, 1);
		List<Vector3> points = geom.GetVertices();
		for(int i=0;i<points.Count;i++)
		{
			_context.CreateEntity()
				.AddGridCell(i,points[i] )
				// .AddResource("PolyText")
				.AddName(string.Format("{0}",i));
		}

		_context.CreateEntity()
            .AddPlanetaryGrid(geom)
            .AddName("PlanetaryGrid")
            .AddResource("PlanetaryGrid");

	}
}