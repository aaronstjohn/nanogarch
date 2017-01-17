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
        var planetEntity = _contexts.core.planetaryGridEntity;

        var spotlightEntity = _contexts.core.GetEntityNamed("CursorSpotlight");

        spotlightEntity.view.gameObject.transform.position=planetEntity.view.gameObject.transform.TransformPoint(input.inputPos.normalized)*0.77f;
        // spotlightEntity.view.gameObject.transform.position=planetEntity.view.gameObject.transform.TransformPoint(focusedItem.planetaryGridPolygon.centroid.normalized)*1.4f;
        
        // spotlightEntity.view.gameObject.transform.LookAt(planetEntity.view.gameObject.transform);
        spotlightEntity.LookAt(planetEntity);
        
        int closestPolyIdx = planetEntity.planetaryGrid.geometry.NearestPoly(input.inputPos);
        var polyEntity = _contexts.core.GetEntityWithPlanetaryGridPolygonId(closestPolyIdx);

        //Take the focus off the last entity that was in focus
        if(_contexts.core.inFocusEntity!=null)
         _contexts.core.inFocusEntity.isInFocus = false;
        
        //Add focus to the new entity in focus 
        polyEntity.isInFocus = true;
       

    }
}