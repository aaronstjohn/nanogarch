using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class ProcessPlanetaryGridInputSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public ProcessPlanetaryGridInputSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
    
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(InputMatcher.Input,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasInput && entity.input.targetName=="PlanetaryGrid";
    }

    protected override void Execute(List<Entity> entities) {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;
        // var spotlightEntity = _contexts.core.GetEntityNamed("FocusSpotlight");
        var planetEntity = _contexts.core.GetEntityNamed("PlanetaryGrid");
        var spotlightEntity = _contexts.core.GetEntityNamed("CursorSpotlight");

        spotlightEntity.view.gameObject.transform.position=input.inputPos.normalized*0.77f;
        spotlightEntity.view.gameObject.transform.LookAt(planetEntity.view.gameObject.transform);
        // var planet = _contexts.core.GetEntityNamed(input.targetName);
        int closestPolyIdx = planetEntity.planetaryGrid.geometry.NearestPoly(input.inputPos);
        var polyEntity = _contexts.core.GetEntityWithPlanetaryGridPolygonId(closestPolyIdx);

        if(_contexts.core.inFocusEntity!=null)
        {
            _contexts.core.inFocusEntity.isInFocus = false;
        }
        polyEntity.isInFocus = true;
        // Debug.Log(string.Format("IN FOCUS ENT ({0})",_contexts.core.inFocusEntity==null?"NULL":"NOT NULL"));
        // Debug.Log("Best poly is poly: "+closestPolyIdx);
        // Debug.Log("got planet input on poly: "+polyEntity.planetaryGridPolygon.polygonId);
        // polyEntity.IsInFocus(true);

    }
}