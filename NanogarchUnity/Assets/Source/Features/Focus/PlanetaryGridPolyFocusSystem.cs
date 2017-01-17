using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class PlanetaryGridPolyFocusSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public PlanetaryGridPolyFocusSystem(Contexts contexts) : base(contexts.core) {
        _contexts = contexts;
    
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.InFocus,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.isInFocus;
    }

    protected override void Execute(List<Entity> entities) {
        var focusedItem = entities.SingleEntity();
        // Debug.Log("Got Focus on pol "+focusedItem.planetaryGridPolygon.polygonId);
        var planetEntity = _contexts.core.GetEntityNamed("PlanetaryGrid");
        var spotlightEntity = _contexts.core.GetEntityNamed("PolygonFocusSpotlight");

        // Vector3 centroid = planetEntity.planetaryGrid.geometry.GetPolyCentroid(focusedItem.planetaryGridPolygon.tris);
        spotlightEntity.view.gameObject.transform.position=focusedItem.planetaryGridPolygon.centroid.normalized*1.4f;
        spotlightEntity.view.gameObject.transform.LookAt(planetEntity.view.gameObject.transform);

        // focusedItem.isInFocus = false;
        // GameObject spotGO = GameObject.Create();
        // var res = Resources.Load<GameObject>("Spot");
        // GameObject spotGO = Object.Instantiate("Spot");
        // var planetGO = planetEntity.view.gameObject;
        // Mesh mesh = planetGO.GetComponent<MeshFilter>().sharedMesh;
        // Vector3[] vertices = mesh.vertices;
        
        // // create new colors array where the colors will be created.
        // Color[] colors = new Color[vertices.Length];
        // for (int i = 0; i < vertices.Length; i++)
        //     colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
        
        // // assign the array of colors to the Mesh.
        // mesh.colors = colors;

    }
}