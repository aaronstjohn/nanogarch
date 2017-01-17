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
        return entity.isInFocus && entity.hasPlanetaryGridPolygon;
    }

    protected override void Execute(List<Entity> entities) {
        var focusedItem = entities.SingleEntity();
        var planetEntity = _contexts.core.planetaryGridEntity;
        var spotlightEntity = _contexts.core.GetEntityNamed("PolygonFocusSpotlight");
        
        spotlightEntity.view.gameObject.transform.position=planetEntity.view.gameObject.transform.TransformPoint(focusedItem.planetaryGridPolygon.centroid.normalized)*1.4f;
        spotlightEntity.LookAt(planetEntity);
    }
}